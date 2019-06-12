using AbstractDishShopModel;
using System.Collections.Generic;


namespace AbstractDishShopServiceImplementList
{
    class DataListSingleton
    {
    private static DataListSingleton instance;
    public List<SClient> Clients { get; set; }
    public List<Materials> Materialss { get; set; }
    public List<SOrder> Orders { get; set; }
    public List<Dish> Dishs { get; set; }
    public List<DishMaterials> DishMaterialss { get; set; }
    public List<Stock> Stocks { get; set; }
    public List<StockMaterials> StockMaterialss { get; set; }
    private DataListSingleton()
    {
        Clients = new List<SClient>();
        Materialss = new List<Materials>();
        Orders = new List<SOrder>();
        Dishs = new List<Dish>();
        DishMaterialss = new List<DishMaterials>();
        Stocks = new List<Stock>();
        StockMaterialss = new List<StockMaterials>();
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