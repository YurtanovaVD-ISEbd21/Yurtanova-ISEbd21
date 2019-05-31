using AbstractDishShopServiceDAL.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveDishPrice(ReportBindingModel model);
        List<StocksLoadViewModel> GetStocksLoad();
        void SaveStocksLoad(ReportBindingModel model);
        List<SClientSOrdersModel> GetSClientSOrders(ReportBindingModel model);
        void SaveSClientSOrders(ReportBindingModel model);
    }
}
