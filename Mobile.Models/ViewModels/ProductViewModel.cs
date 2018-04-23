using System.Collections.Generic;

namespace Mobile.Models.ViewModels
{
    public class ProductViewModel : SearchProductViewModel
    {
        public string DiscountAccompanying { get; set; }
        public string LargeImage { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductDetailViewModel : SearchProductViewModel
    {
        public string Code { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> MoreImages { get; set; }
        public bool IncludeVAT { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }
    }

    public class RelatedProductViewModel : SearchProductViewModel
    {
        public string Screen { get; set; }
        public string CameraAfter { get; set; }
        public string CameraBefore { get; set; }
        public string PinCapacity { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
    }
}
