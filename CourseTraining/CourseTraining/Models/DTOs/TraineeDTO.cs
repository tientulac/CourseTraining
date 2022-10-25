using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseTraining.Models.DTOs
{
    public class TraineeDTO: Trainee
    {
        public Account Account { get; set; }
        public string GenderName { get; set; }
        public string UserName { get; set; }
    }
}