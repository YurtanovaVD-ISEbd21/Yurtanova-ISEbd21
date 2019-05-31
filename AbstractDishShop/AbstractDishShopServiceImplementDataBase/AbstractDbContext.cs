using AbstractDishShopModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceImplementDataBase
{
    public class AbstractDbContext : DbContext
    {

        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<SClient> SClients { get; set; }
        public virtual DbSet<Materials> Materialss { get; set; }
        public virtual DbSet<SOrder> SOrders { get; set; }
        public virtual DbSet<Dish> Dishs { get; set; }
        public virtual DbSet<DishMaterials> DishMaterialss { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockMaterials> StockMaterialss { get; set; }
    }

}
