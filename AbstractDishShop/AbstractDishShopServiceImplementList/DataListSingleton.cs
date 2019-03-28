using AbstractDishShopModel;
using System.Collections.Generic;


namespace AbstractDishShopServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<SClient> SClients { get; set; }
        public List<Materials> Materialss { get; set; }
        public List<SOrder> SOrders { get; set; }
        public List<Dish> Dishs { get; set; }
        public List<DishMaterials> DishMaterialss { get; set; }
        private DataListSingleton()
        {
            SClients = new List<SClient>();
            Materialss = new List<Materials>();
            SOrders = new List<SOrder>();
            Dishs = new List<Dish>();
            DishMaterialss = new List<DishMaterials>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}