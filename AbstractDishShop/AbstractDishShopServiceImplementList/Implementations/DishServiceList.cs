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
            List<DishViewModel> result = new List<DishViewModel>();
            for (int i = 0; i < source.Dishs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их  количество
                List<DishMaterialsViewModel> DishMaterialss = new List<DishMaterialsViewModel>();
                for (int j = 0; j < source.DishMaterialss.Count; ++j)
                {
                    if (source.DishMaterialss[j].DishId == source.Dishs[i].Id)
                    {
                        string materialsName = string.Empty;
                        for (int k = 0; k < source.Materialss.Count; ++k)
                        {
                            if (source.DishMaterialss[j].MaterialsId ==
                           source.Materialss[k].Id)
                            {
                                materialsName = source.Materialss[k].MaterialsName;
                                break;
                            }
                        }
                        DishMaterialss.Add(new DishMaterialsViewModel
                        {
                            Id = source.DishMaterialss[j].Id,
                            DishId = source.DishMaterialss[j].DishId,
                            MaterialsId = source.DishMaterialss[j].MaterialsId,
                            MaterialsName = materialsName,
                            Count = source.DishMaterialss[j].Count
                        });
                    }
                }
                result.Add(new DishViewModel
                {
                    Id = source.Dishs[i].Id,
                    DishName = source.Dishs[i].DishName,
                    Price = source.Dishs[i].Price,
                    DishMaterials = DishMaterialss
                });
            }
            return result;
        }
        public DishViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Dishs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<DishMaterialsViewModel> DishMaterialss = new List<DishMaterialsViewModel>();
                for (int j = 0; j < source.DishMaterialss.Count; ++j)
                {
                    if (source.DishMaterialss[j].DishId == source.Dishs[i].Id)
                    {
                        string materialsName = string.Empty;
                        for (int k = 0; k < source.Materialss.Count; ++k)
                        {
                            if (source.DishMaterialss[j].MaterialsId ==
                           source.Materialss[k].Id)
                            {
                                materialsName = source.Materialss[k].MaterialsName;
                                break;
                            }
                        }
                        DishMaterialss.Add(new DishMaterialsViewModel
                        {
                            Id = source.DishMaterialss[j].Id,
                            DishId = source.DishMaterialss[j].DishId,
                            MaterialsId = source.DishMaterialss[j].MaterialsId,
                            MaterialsName = materialsName,
                            Count = source.DishMaterialss[j].Count
                        });
                    }
                }
                if (source.Dishs[i].Id == id)
                {
                    return new DishViewModel
                    {
                        Id = source.Dishs[i].Id,
                        DishName = source.Dishs[i].DishName,
                        Price = source.Dishs[i].Price,
                        DishMaterials = DishMaterialss
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(DishBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Dishs.Count; ++i)
            {
                if (source.Dishs[i].Id > maxId)
                {
                    maxId = source.Dishs[i].Id;
                }
                if (source.Dishs[i].DishName == model.DishName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Dishs.Add(new Dish
            {
                Id = maxId + 1,
                DishName = model.DishName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.DishMaterialss.Count; ++i)
            {
                if (source.DishMaterialss[i].Id > maxPCId)
                {
                    maxPCId = source.DishMaterialss[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.DishMaterialss.Count; ++i)
            {
                for (int j = 1; j < model.DishMaterialss.Count; ++j)
                {
                    if (model.DishMaterialss[i].MaterialsId ==
                    model.DishMaterialss[j].MaterialsId)
                    {
                        model.DishMaterialss[i].Count +=
                        model.DishMaterialss[j].Count;
                        model.DishMaterialss.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.DishMaterialss.Count; ++i)
            {
                source.DishMaterialss.Add(new DishMaterials
                {
                    Id = ++maxPCId,
                    DishId = maxId + 1,
                    MaterialsId = model.DishMaterialss[i].MaterialsId,
                    Count = model.DishMaterialss[i].Count
                });
            }
        }
        public void UpdElement(DishBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Dishs.Count; ++i)
            {
                if (source.Dishs[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Dishs[i].DishName == model.DishName &&
                source.Dishs[i].Id != model.Id)
                {
                    throw new Exception("Уже есть Блюдо с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Dishs[index].DishName = model.DishName;
            source.Dishs[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.DishMaterialss.Count; ++i)
            {
                if (source.DishMaterialss[i].Id > maxPCId)
                {
                    maxPCId = source.DishMaterialss[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.DishMaterialss.Count; ++i)
            {
                if (source.DishMaterialss[i].DishId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.DishMaterialss.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.DishMaterialss[i].Id ==
                       model.DishMaterialss[j].Id)
                        {
                            source.DishMaterialss[i].Count =
                           model.DishMaterialss[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.DishMaterialss.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.DishMaterialss.Count; ++i)
            {
                if (model.DishMaterialss[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.DishMaterialss.Count; ++j)
                    {
                        if (source.DishMaterialss[j].DishId == model.Id &&
                        source.DishMaterialss[j].MaterialsId ==
                       model.DishMaterialss[i].MaterialsId)
                        {
                            source.DishMaterialss[j].Count +=
                           model.DishMaterialss[i].Count;
                            model.DishMaterialss[i].Id =
                           source.DishMaterialss[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.DishMaterialss[i].Id == 0)
                    {
                        source.DishMaterialss.Add(new DishMaterials
                        {
                            Id = ++maxPCId,
                            DishId = model.Id,
                            MaterialsId = model.DishMaterialss[i].MaterialsId,
                            Count = model.DishMaterialss[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.DishMaterialss.Count; ++i)
            {
                if (source.DishMaterialss[i].DishId == id)
                {
                    source.DishMaterialss.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Dishs.Count; ++i)
            {
                if (source.Dishs[i].Id == id)
                {
                    source.Dishs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}