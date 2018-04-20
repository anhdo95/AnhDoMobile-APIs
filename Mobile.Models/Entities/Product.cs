using Mobile.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string Code { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Required, StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        [Required, Range(0, 100000000)]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public bool IncludeVAT { get; set; }

        [Required, Range(0, 1000)]
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? TopHot { get; set; }

        public int ViewCount { get; set; }

        public bool Status { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ProductSpecification Specification { get; set; }

        [ForeignKey("User")]
        public int CreatedBy { get; set; }
        public virtual User User { get; set; }
    }
}
