using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TopicTraineeController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        public ActionResult Insert(TraineeTopic req)
        {
            db.TraineeTopics.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int TraineeTopicId)
        {
            var _TraineeTopic = db.TraineeTopics.Where(x => x.TraineeTopicId == TraineeTopicId).FirstOrDefault();
            db.TraineeTopics.DeleteOnSubmit(_TraineeTopic);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}