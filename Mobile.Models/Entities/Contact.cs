using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, RegularExpression(@"^(\+84|0)\d{9,10}$")]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }
    }
}
