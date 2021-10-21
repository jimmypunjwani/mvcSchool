using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcSchool.Models
{
    public class EnrollmentMetadata
    {
        [Display(Name = "Grade")]
        public Nullable<decimal> Grade { get; set; }
        
        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [Display(Name = "Course")]
        public virtual Course Course { get; set; }

        [Display(Name = "Student")]
        public int StudentID { get; set; }

        [Display(Name = "Student")]
        public virtual Student Student { get; set; }

        [Display(Name = "Lecturer")]
        public Nullable<int> LecturerID { get; set; }

        [Display(Name = "Lecturer")]
        public virtual Lecturer Lecturer { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment
    {

    }
}