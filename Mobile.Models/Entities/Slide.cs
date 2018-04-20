using Mobile.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mobile.Models.Entities
{
    public class Slide
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Image { get; set; }

        public byte? DisplayOrder { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public TargetLink Target { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Status { get; set; }

        public SlidePosition Position { get; set; }
    }
}
