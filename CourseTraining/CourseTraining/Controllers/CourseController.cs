using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class CourseController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Course req)
        {
            if (req.CourseId > 0)
            {
                var _course = db.Courses.Where(M => M.CourseId == req.CourseId).FirstOrDefault();
                _course.CourseName = req.CourseName;
                _course.Image = req.Image;
                _course.Descrip = req.Descrip;
                _course.Status = req.Status;
                _course.CategoryId = req.CategoryId;
                _course.Author = req.Author;
                _course.Forum = req.Forum;

                db.SubmitChanges();
                return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                req.Status = 1;
            }
            db.Courses.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int courseId)
        {
            var _course = db.Courses.Where(M => M.CourseId == courseId).FirstOrDefault();
            db.Courses.DeleteOnSubmit(_course);
            db.SubmitChanges();
            return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int courseId)
        {
            var _course = db.Courses.Where(M => M.CourseId == courseId).FirstOrDefault();
            return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bouqueti
        public ActionResult Index()
        {
            var listCourse = (from a in db.Courses
                              select new CourseDTO {
                                  CourseId = a.CourseId,
                                  CourseName = a.CourseName,
                                  Image = a.Image,
                                  Descrip = a.Descrip,
                                  Status = a.Status,
                                  CategoryId = a.CategoryId,
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? "",
                                  Author = a.Author,
                                  Forum = a.Forum
                              }).ToList();
            ViewBag.ListCourse = listCourse;
            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }
    }
}