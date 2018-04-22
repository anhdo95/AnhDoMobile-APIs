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
        public string Image { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public decimal Price { get; set; }
    }
}
