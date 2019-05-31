using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceImplementDataBase
{
    public class MaterialsServiceBD : IMaterialsService
    {
        private AbstractDbContext context;

        public MaterialsServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<MaterialsViewModel> GetList()
        {
            List<MaterialsViewModel> result = context.Materialss
                .Select(rec => new MaterialsViewModel
                {
                    Id = rec.Id,
                    MaterialsName = rec.MaterialsName
                })
                .ToList();
            return result;
        }

        public MaterialsViewModel GetElement(int id)
        {
            Materials element = context.Materialss.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MaterialsViewModel
                {
                    Id = element.Id,
                    MaterialsName = element.MaterialsName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(MaterialsBindingModel model)
        {
            Materials element = context.Materialss.FirstOrDefault(rec => rec.MaterialsName == model.MaterialsName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Materialss.Add(new Materials
            {
                MaterialsName = model.MaterialsName
            });
            context.SaveChanges();
        }

        public void UpdElement(MaterialsBindingModel model)
        {
            Materials element = context.Materialss.FirstOrDefault(rec =>
                                        rec.MaterialsName == model.MaterialsName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = context.Materialss.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.MaterialsName = model.MaterialsName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Materials element = context.Materialss.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Materialss.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}