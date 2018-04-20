using System;
using System.ComponentModel.DataAnnotations;

namespace Mobile.Models.Entities
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }

        [StringLength(100)]
        public string CartId { get; set; }

        [Required, Range(0, 100000000)]
        public decimal Price { get; set; }

        [Required, Range(0, 100)]
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
