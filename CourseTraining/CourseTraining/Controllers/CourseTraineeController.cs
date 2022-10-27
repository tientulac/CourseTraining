using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class CourseTraineeController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        public ActionResult Insert(TraineeCourse req)
        {
            db.TraineeCourses.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int traineeCourseId)
        {
            var _traineeCourse = db.TraineeCourses.Where(x => x.TraineeCourseId == traineeCourseId).FirstOrDefault();
            db.TraineeCourses.DeleteOnSubmit(_traineeCourse);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}