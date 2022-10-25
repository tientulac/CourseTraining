using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseTraining.Models.DTOs
{
    public class CourseDTO: Course
    {
        public string CategoryName { get; set; }
    }
}