using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcSchool.Models
{
    public class CourseMetadata
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }
        
        [Required]
        [Range(1, 8)]
        public int Credits { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course
    {

    }
}