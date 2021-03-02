using System.Collections.Generic;

namespace FastFood.Web.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        public List<CreateOrderItemViewModel> Items { get; set; }

        public List<CreateOrderEmployeeViewModel> Employees { get; set; }
    }
}
