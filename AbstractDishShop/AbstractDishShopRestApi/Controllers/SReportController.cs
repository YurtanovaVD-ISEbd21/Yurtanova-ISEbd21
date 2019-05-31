using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractDishShopRestApi.Controllers
{
    public class SReportController : ApiController
    {
        private readonly IReportService _service;
        public SReportController(IReportService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetStocksLoad()
        {
            var list = _service.GetStocksLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult GetSClientOrders(ReportBindingModel model)
        {
            var list = _service.GetSClientSOrders(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void SaveDishPrice(ReportBindingModel model)
        {
            _service.SaveDishPrice(model);
        }
        [HttpPost]
        public void SaveStocksLoad(ReportBindingModel model)
        {
            _service.SaveStocksLoad(model);
        }
        [HttpPost]
        public void SaveClientOrders(ReportBindingModel model)
        {
            _service.SaveSClientSOrders(model);
        }
    }
}