using Mobile.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mobile.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [RegularExpression(@"^(\+84|0)\d{9,10}$")]
        public string PhoneNumber { get; set; }

        public UserStatus Status { get; set; }
    }
}