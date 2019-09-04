using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace AbstractDishShopServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private AbstractDbContext context;
        public MainServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<SOrderViewModel> GetList()
        {
            List<SOrderViewModel> result = context.SOrders.Select(rec => new SOrderViewModel
            {
                Id = rec.Id,
                SClientId = rec.SClientId,
                DishId = rec.DishId,
                ImplementerId = rec.ImplementerId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                SClientFIO = rec.SClient.SClientFIO,
                DishName = rec.Dish.DishName,
                ImplementerName = rec.Implementer.ImplementerName
            })
            .ToList();
            return result;
        }
        public List<SOrderViewModel> GetFreeSOrders()
        {
            List<SOrderViewModel> result = context.SOrders
            .Where(x => x.Status == SOrderStatus.Принят || x.Status == SOrderStatus.НедостаточноРесурсов)
            .Select(rec => new SOrderViewModel
            {
                Id = rec.Id
            })
            .ToList();
            return result;
        }
        public void CreateSOrder(SOrderBindingModel model)
        {
            var SOrder = new SOrder
            {
                SClientId = model.SClientId,
                DishId = model.DishId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = SOrderStatus.Принят
            };
            
        context.SOrders.Add(SOrder);
            context.SaveChanges();
            var SClient = context.SClients.FirstOrDefault(x => x.Id == model.SClientId);
            SendEmail(SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", SOrder.Id, SOrder.DateCreate.ToShortDateString()));
        }
        public void TakeSOrderInWork(SOrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
                try
                {
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.Status != SOrderStatus.Принят && element.Status != SOrderStatus.НедостаточноРесурсов)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var DishMaterials = context.DishMaterialss.Include(rec => rec.Materials).Where(rec => rec.DishId == element.DishId);
                    // списываем
                    foreach (var DishMaterial in DishMaterials)
                    {
                        int countOnStocks = DishMaterial.Count * element.Count;
                        var stockMaterials = context.StockMaterialss.Where(rec => rec.MaterialsId == DishMaterial.MaterialsId);
                        foreach (var stockMaterial in stockMaterials)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (stockMaterial.Count >= countOnStocks)
                            {
                                stockMaterial.Count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockMaterial.Count;
                                stockMaterial.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " + DishMaterial.Materials.MaterialsName + " требуется " + DishMaterial.Count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.ImplementerId = model.ImplementerId;
                    element.DateImplement = DateTime.Now;
                    element.Status = SOrderStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                    
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    element.Status = SOrderStatus.НедостаточноРесурсов;
                    context.SaveChanges();
                    transaction.Commit();
                    throw;
                }
            }
        }
        public void FinishSOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = SOrderStatus.Готов;
            context.SaveChanges();
            SendEmail(element.SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PaySOrder(SOrderBindingModel model)
        {
            SOrder element = context.SOrders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != SOrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = SOrderStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.SClient.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.DateCreate.ToShortDateString()));
        }
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            StockMaterials element = context.StockMaterialss.FirstOrDefault(rec => rec.StockId == model.StockId && rec.MaterialsId == model.MaterialsId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StockMaterialss.Add(new StockMaterials
                {
                    StockId = model.StockId,
                    MaterialsId = model.MaterialsId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
            
        }
        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpSClient = null;
            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objSmtpSClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpSClient.UseDefaultCredentials = false;
                objSmtpSClient.EnableSsl = true;
                objSmtpSClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpSClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"], ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpSClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpSClient = null;
            }
        }

        
    }
}