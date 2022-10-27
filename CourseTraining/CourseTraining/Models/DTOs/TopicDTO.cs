using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseTraining.Models.DTOs
{
    public class TopicDTO : Topic
    {
        public string CategoryName { get; set; }
        public int TrainerId { get; set; }
        public int TrainerTopicId { get; set; }
        public int TraineeId { get; set; }
        public int TraineeTopicId { get; set; }
    }
}