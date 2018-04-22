using System.Collections.Generic;

namespace Mobile.Models.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItemViewModel> CartItems { get; set; }
        public decimal CartTotalPrice { get; set; }
    }

    public class CartItemViewModel
    {
        public int RecordId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductMetaTitle { get; set; }
        public decimal Price { get; set; }
    }
}
