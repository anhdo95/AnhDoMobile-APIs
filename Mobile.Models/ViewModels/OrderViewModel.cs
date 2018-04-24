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
        public IEnumerable<OrderDetailViewModel> OrderItems { get; set; }
    }

    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProvinceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DistrictViewModel : ProvinceViewModel
    {
        public int ProvinceId { get; set; }
    }
}
