using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AbstractDishShopServiceImplementDataBase.Implementations
{
    public class DishServiceDB : IDishService
    {
        private AbstractDbContext context;
        public DishServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<DishViewModel> GetList()
        {
            List<DishViewModel> result = context.Dishs
                .Select(rec => new DishViewModel
                {
                    Id = rec.Id,
                    DishName = rec.DishName,
                    Price = rec.Price,
                    DishMaterials = context.DishMaterialss
                            .Where(recPC => recPC.DishId == rec.Id)
                            .Select(recPC => new DishMaterialsViewModel
                            {
                                Id = recPC.Id,
                                DishId = recPC.DishId,
                                MaterialsId = recPC.MaterialsId,
                                MaterialsName = recPC.Materials.MaterialsName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public DishViewModel GetElement(int id)
        {
            Dish element = context.Dishs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new DishViewModel
                {
                    Id = element.Id,
                    Price = element.Price,
                    DishName = element.DishName,
                    DishMaterials = context.DishMaterialss
                            .Where(recPC => recPC.DishId == element.Id)
                            .Select(recPC => new DishMaterialsViewModel
                            {
                                Id = recPC.Id,
                                DishId = recPC.DishId,
                                MaterialsId = recPC.MaterialsId,
                                MaterialsName = recPC.Materials.MaterialsName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(DishBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Dish element = context.Dishs.FirstOrDefault(rec => rec.DishName == model.DishName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Dish
                    {
                        DishName = model.DishName,
                        Price = model.Price
                    };
                    context.Dishs.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupMaterialss = model.DishMaterialss
                    .GroupBy(rec => rec.MaterialsId)
                    .Select(rec => new
                    {
                        MaterialsId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupMaterials in groupMaterialss)
                    {
                        context.DishMaterialss.Add(new DishMaterials
                        {
                            DishId = element.Id,
                            MaterialsId = groupMaterials.MaterialsId,
                            Count = groupMaterials.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                   
                }
            }
        }
        public void UpdElement(DishBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Dish element = context.Dishs.FirstOrDefault(rec => rec.DishName == model.DishName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Dishs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.DishName = model.DishName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.DishMaterialss.Select(rec => rec.MaterialsId).Distinct();
                    var updateMaterialss = context.DishMaterialss.Where(rec => rec.DishId == model.Id && compIds.Contains(rec.MaterialsId));
                    foreach (var updateMaterials in updateMaterialss)
                    {
                        updateMaterials.Count = model.DishMaterialss.FirstOrDefault(rec => rec.Id == updateMaterials.Id).Count;
                    }
                    context.SaveChanges();
                    context.DishMaterialss.RemoveRange(context.DishMaterialss.Where(rec => rec.DishId == model.Id && !compIds.Contains(rec.MaterialsId)));
                    context.SaveChanges();
                    // новые записи
                    var groupMaterialss = model.DishMaterialss
                    .Where(rec => rec.Id == 0)
                    .GroupBy(rec => rec.MaterialsId)
                    .Select(rec => new
                    {
                        MaterialsId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    foreach (var groupMaterials in groupMaterialss)
                    {
                        DishMaterials elementPC = context.DishMaterialss.FirstOrDefault(rec => rec.DishId == model.Id && rec.MaterialsId == groupMaterials.MaterialsId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupMaterials.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.DishMaterialss.Add(new DishMaterials
                            {
                                DishId = model.Id,
                                
                            MaterialsId = groupMaterials.MaterialsId,
                                Count = groupMaterials.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Dish element = context.Dishs.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.DishMaterialss.RemoveRange(context.DishMaterialss.Where(rec => rec.DishId == id));
                        context.Dishs.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
