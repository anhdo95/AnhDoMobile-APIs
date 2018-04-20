using System.ComponentModel.DataAnnotations;

namespace Mobile.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public bool Gender { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, RegularExpression(@"^(\+84|0)\d{9,10}$")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }
    }
}
