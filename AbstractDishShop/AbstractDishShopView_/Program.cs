using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceImplementList.Implementations;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractDishShopView
{
    static class Program
    {/// <summary>
     /// Главная точка входа для приложения.
     /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ISClientService, SClientServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialsService, MaterialsServiceList>(new
 HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDishService, DishServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceList>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}