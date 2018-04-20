using System.ComponentModel.DataAnnotations;
using Mobile.Common.Enums;

namespace Mobile.Models.Entities
{
    public class Menu
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Text { get; set; }

        [StringLength(100)]
        public string Link { get; set; }

        public byte? DisplayOrder { get; set; }

        public TargetLink Target { get; set; }

        public bool Status { get; set; }

        [StringLength(100)]
        public string Icon { get; set; }
    }
}
