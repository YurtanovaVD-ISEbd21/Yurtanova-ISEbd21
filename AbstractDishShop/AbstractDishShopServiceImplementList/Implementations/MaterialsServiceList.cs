using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AbstractDishShopServiceImplementList.Implementations
{
    public class MaterialsServiceList : IMaterialsService
    {
        private DataListSingleton source;
        public MaterialsServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MaterialsViewModel> GetList()
        {
            List<MaterialsViewModel> result = source.Materialss.Select(rec => new MaterialsViewModel
            {
                Id = rec.Id,
                MaterialsName = rec.MaterialsName
            })
            .ToList();
            return result;
        }
        public MaterialsViewModel GetElement(int id)
        {
            Materials element = source.Materialss.FirstOrDefault(rec => rec.Id == id);
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
            Materials element = source.Materialss.FirstOrDefault(rec => rec.MaterialsName == model.MaterialsName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Materialss.Count > 0 ? source.Materialss.Max(rec => rec.Id) : 0;
            source.Materialss.Add(new Materials
            {
                Id = maxId + 1,
                MaterialsName = model.MaterialsName
            });
        }
        public void UpdElement(MaterialsBindingModel model)
        {
            Materials element = source.Materialss.FirstOrDefault(rec => rec.MaterialsName == model.MaterialsName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Materialss.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
                
            }
            element.MaterialsName = model.MaterialsName;
        }
        public void DelElement(int id)
        {
            Materials element = source.Materialss.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Materialss.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}