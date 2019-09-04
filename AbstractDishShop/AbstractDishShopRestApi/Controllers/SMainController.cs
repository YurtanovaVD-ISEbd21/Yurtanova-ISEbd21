using AbstractDishShopRestApi.Services;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AbstractDishShopRestApi.Controllers
{
    public class SMainController : ApiController
    {
        private readonly IMainService _service;
        private readonly IImplementerService _serviceImplementer;
        public SMainController(IMainService service, IImplementerService
       serviceImplementer)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
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
        public void CreateSOrder(SOrderBindingModel model)
        {
            _service.CreateSOrder(model);
        }
        [HttpPost]
        public void PaySOrder(SOrderBindingModel model)
        {
            _service.PaySOrder(model);
        }
        [HttpPost]
        public void PutMaterialsOnStock(StockMaterialsBindingModel model)
        {
            _service.PutMaterialsOnStock(model);
        }
        [HttpPost]
        public void StartWork()
        {
            List<SOrderViewModel> orders = _service.GetFreeSOrders();
            foreach (var order in orders)
            {
                ImplementerViewModel impl = _serviceImplementer.GetFreeImplementer();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkSImplementer(_service, _serviceImplementer, impl.Id, order.Id);
            }
        }
    }
}