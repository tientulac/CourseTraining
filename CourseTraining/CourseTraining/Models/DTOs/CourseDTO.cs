using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseTraining.Models.DTOs
{
    public class CourseDTO: Course
    {
        public string CategoryName { get; set; }
        public int TrainerId { get; set; }
        public int TrainerCourseId { get; set; }
        public int TraineeId { get; set; }
        public int TraineeCourseId { get; set; }
        public int StaffId { get; set; }
        public int StaffCourseId { get; set; }
    }
}