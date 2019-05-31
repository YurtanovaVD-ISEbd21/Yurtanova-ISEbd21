using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceImplementDataBase;
using AbstractDishShopServiceImplementDataBase.Implementations;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractDishShopView
{
    static class Program
    {
        /// <summary>
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
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISClientService, SClientServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialsService, MaterialsServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDishService, DishServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}