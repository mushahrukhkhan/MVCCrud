using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCrud.Models
{
    public class StudentInfoModel
    {

        public Int32 Id { get; set; }

        [Required]
        public String Code { get; set; }
        public String Name { get; set; }

        [Required]
        [Display(Name = "Class")]

        public Int32 ClassId { get; set; }
        [Required]
        [Display(Name = "Course")]

        public Int32 CourseId { get; set; }
        [Required]
        [Display(Name = "Contact")]

        public String Contact { get; set; }

        public String ClassName { get; set; }
        public String CourseName { get; set; }

    }
}