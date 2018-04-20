using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required, Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
