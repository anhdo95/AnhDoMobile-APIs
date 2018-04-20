using Mobile.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mobile.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string ShipName { get; set; }

        public bool ShipGender { get; set; }

        [Required, RegularExpression(@"^(\+84|0)\d{9,10}$")]
        public string ShipMobile { get; set; }

        [Required, StringLength(250)]
        public string ShipAddress { get; set; }

        public OrderStatus Status { get; set; }

        [Required, Range(0, 1000000000)]
        public decimal Total { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
