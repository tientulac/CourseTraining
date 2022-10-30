using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class CourseStaffController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        public ActionResult Insert(StaffCourse req)
        {
            db.StaffCourses.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int staffCourseId)
        {
            var _staffCourse = db.StaffCourses.Where(x => x.StaffCourseId == staffCourseId).FirstOrDefault();
            db.StaffCourses.DeleteOnSubmit(_staffCourse);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}