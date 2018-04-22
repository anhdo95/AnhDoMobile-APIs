using Mobile.Models.Entities;
using System.Collections.Generic;

namespace Mobile.Models.ViewModels
{
    public class OrderCompleteViewModel
    {
        public int OrderId { get; set; }
        public bool ShipGender { get; set; }
        public string ShipName { get; set; }
        public string ShipPhone { get; set; }
        public string ShipAddress { get; set; }
        public decimal OrderTotal { get; set; }
        public IEnumerable<OrderDetail> OrderItems { get; set; }
    }
}
