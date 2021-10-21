using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcSchool.Models
{
    public class StudentCourse
    {
        //Adding Course Fields from Enrollment:
        [Display(Name = "CourseID")]
        public int CourseID { get; set; }

        [Display(Name = "EnrollmentID")]
        public int EnrollmentID { get; set; }

        [Display(Name = "StudentID")]
        public int StudentID { get; set; }

        [Display(Name = "Course")]
        public virtual Course Course { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
    }
}