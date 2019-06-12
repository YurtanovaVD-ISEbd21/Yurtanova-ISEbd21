using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractDishShopServiceDAL.ViewModel
{
    public class SOrderViewModel
    {
        public int Id { get; set; }
        public int SClientId { get; set; }
        [DisplayName("ФИО Клиента")]
        public string SClientFIO { get; set; }
        public int DishId { get; set; }
        [DisplayName("Блюдо")]
        public string DishName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
