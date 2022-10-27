using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class CourseTrainerController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        public ActionResult Insert(TrainerCourse req)
        {
            db.TrainerCourses.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int trainerCourseId)
        {
            var _trainerCourse = db.TrainerCourses.Where(x => x.TrainerCourseId == trainerCourseId).FirstOrDefault();
            db.TrainerCourses.DeleteOnSubmit(_trainerCourse);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}