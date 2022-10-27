using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TopicTrainerController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        public ActionResult Insert(TrainerTopic req)
        {
            db.TrainerTopics.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int trainerTopicId)
        {
            var _trainerTopic = db.TrainerTopics.Where(x => x.TrainerTopicId == trainerTopicId).FirstOrDefault();
            db.TrainerTopics.DeleteOnSubmit(_trainerTopic);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}