using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractDishShopRestApi.Controllers
{
    public class SMainController : ApiController
    {
        private readonly IMainService _service;
        public SMainController(IMainService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void CreateOrder(SOrderBindingModel model)
        {
            _service.CreateSOrder(model);
        }
        [HttpPost]
        public void TakeOrderInWork(SOrderBindingModel model)
        {
            _service.TakeSOrderInWork(model);
        }
        [HttpPost]

        public void FinishOrder(SOrderBindingModel model)
        {
            _service.FinishSOrder(model);
        }
        [HttpPost]
        public void PayOrder(SOrderBindingModel model)
        {
            _service.PaySOrder(model);
        }
        [HttpPost]
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            _service.PutMaterialsOnStock(model);
        }
    }
}