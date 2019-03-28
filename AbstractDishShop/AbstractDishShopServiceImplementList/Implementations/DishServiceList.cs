using AbstractDishShopModel;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceImplementList.Implementations
{
    public class DishServiceList : IDishService
    {
        private DataListSingleton source;
        public DishServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetList()
        {
            List<DishViewModel> result = source.Dishs
            .Select(rec => new DishViewModel
            {
                Id = rec.Id,
                DishName = rec.DishName,
                Price = rec.Price,
                DishMaterials = source.DishMaterialss
            .Where(recPC => recPC.DishId == rec.Id)
            .Select(recPC => new DishMaterialsViewModel
            {
                Id = recPC.Id,
                DishId = recPC.DishId,
                MaterialsId = recPC.MaterialsId,
                MaterialsName = source.Materialss.FirstOrDefault(recC => recC.Id == recPC.MaterialsId)?.MaterialsName,
                Count = recPC.Count
            })
            .ToList()
            })
            .ToList();
            
        return result;
        }
        public DishViewModel GetElement(int id)
        {
            Dish element = source.Dishs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new DishViewModel
                {
                    Id = element.Id,
                    DishName = element.DishName,
                    Price = element.Price,
                   DishMaterials = source.DishMaterialss
                .Where(recPC => recPC.DishId == element.Id)
                .Select(recPC => new DishMaterialsViewModel
                {
                    Id = recPC.Id,
                    DishId = recPC.DishId,
                    MaterialsId = recPC.MaterialsId,
                    MaterialsName = source.Materialss.FirstOrDefault(recC => recC.Id == recPC.MaterialsId)?.MaterialsName,
                    Count = recPC.Count
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(DishBindingModel model)
        {
            Dish element = source.Dishs.FirstOrDefault(rec => rec.DishName == model.DishName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Dishs.Count > 0 ? source.Dishs.Max(rec => rec.Id) : 0;
            source.Dishs.Add(new Dish
            {
                Id = maxId + 1,
                DishName = model.DishName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = source.DishMaterialss.Count > 0 ? source.DishMaterialss.Max(rec => rec.Id) : 0;
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
                source.DishMaterialss.Add(new DishMaterials
                {
                    Id = ++maxPCId,
                    DishId = maxId + 1,
                    
                MaterialsId = groupMaterials.MaterialsId,
                    Count = groupMaterials.Count
                });
            }
        }
        public void UpdElement(DishBindingModel model)
        {
            Dish element = source.Dishs.FirstOrDefault(rec => rec.DishName == model.DishName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Dishs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.DishName = model.DishName;
            element.Price = model.Price;
            int maxPCId = source.DishMaterialss.Count > 0 ? source.DishMaterialss.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.DishMaterialss.Select(rec => rec.MaterialsId).Distinct();
            var updateMaterialss = source.DishMaterialss.Where(rec => rec.DishId == model.Id && compIds.Contains(rec.MaterialsId));
            foreach (var updateMaterials in updateMaterialss)
            {
                updateMaterials.Count = model.DishMaterialss.FirstOrDefault(rec => rec.Id == updateMaterials.Id).Count;
            }
            source.DishMaterialss.RemoveAll(rec => rec.DishId == model.Id && !compIds.Contains(rec.MaterialsId));
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
                DishMaterials elementPC = source.DishMaterialss.FirstOrDefault(rec => rec.DishId == model.Id && rec.MaterialsId == groupMaterials.MaterialsId);
                if (elementPC != null)
                {
                    elementPC.Count += groupMaterials.Count;
                }
                else
                {
                    source.DishMaterialss.Add(new DishMaterials
                    {
                        Id = ++maxPCId,
                        DishId = model.Id,
                        MaterialsId = groupMaterials.MaterialsId,
                        Count = groupMaterials.Count
                    });
                }
            }
          
        }
        public void DelElement(int id)
        {
            Dish element = source.Dishs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.DishMaterialss.RemoveAll(rec => rec.DishId == id);
                source.Dishs.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}