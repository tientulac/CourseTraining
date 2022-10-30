using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TopicStaffController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        public ActionResult Insert(StaffTopic req)
        {
            db.StaffTopics.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int StaffTopicId)
        {
            var _StaffTopic = db.StaffTopics.Where(x => x.StaffTopicId == StaffTopicId).FirstOrDefault();
            db.StaffTopics.DeleteOnSubmit(_StaffTopic);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}