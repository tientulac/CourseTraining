using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseTraining.Models.DTOs
{
    public class TrainerDTO: Trainer
    {
        public Account Account { get; set; }
        public string UserName { get; set; }
        public string GenderName { get; set; }
    }
}