using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class StaffController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(StaffDTO req)
        {
            if (req.StaffId > 0)
            {
                var _staff = db.Staffs.Where(M => M.StaffId == req.StaffId).FirstOrDefault();
                _staff.StaffName = req.StaffName;
                _staff.Dob = req.Dob;
                _staff.Image = req.Image;
                _staff.Gender = req.Gender;
                _staff.Position = req.Position;
                _staff.Phone = req.Phone;
                var _userUpdate = db.Accounts.Where(M => M.AccountId == _staff.AccountId).FirstOrDefault();
                _userUpdate.Email = req.Account.Email;
                _userUpdate.Admin = req.Account.Admin;

                db.SubmitChanges();
                return Json(new { success = true, data = _staff }, JsonRequestBehavior.AllowGet);
            }

            var _user = new Account();
            _user.UserName = req.Account.UserName;
            _user.Email = req.Account.Email;
            _user.Active = true;
            _user.Admin = req.Account.Admin;
            _user.Password = req.Account.Password;
            db.Accounts.InsertOnSubmit(_user);
            db.SubmitChanges();

            var _staffInsert = new Staff();
            _staffInsert.StaffName = req.StaffName;
            _staffInsert.Dob = req.Dob;
            _staffInsert.Image = req.Image;
            _staffInsert.Gender = req.Gender;
            _staffInsert.AccountId = _user.AccountId;
            _staffInsert.Phone = req.Phone;
            _staffInsert.Position = req.Position;

            db.Staffs.InsertOnSubmit(_staffInsert);
            db.SubmitChanges();

            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int staffId)
        {
            var _staff = db.Staffs.Where(M => M.StaffId == staffId).FirstOrDefault();
            db.Staffs.DeleteOnSubmit(_staff);
            db.SubmitChanges();
            return Json(new { success = true, data = _staff }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int staffId)
        {
            var _Staff = (from a in db.Staffs.Where(x => x.StaffId == staffId)
                            select new StaffDTO
                            {
                                Account = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault(),
                                StaffId = a.StaffId,
                                StaffName = a.StaffName,
                                Dob = a.Dob,
                                Image = a.Image,
                                Gender = a.Gender,
                                AccountId = a.AccountId,
                                Position = a.Position,
                                Phone = a.Phone,
                                GenderName = a.Gender == true ? "Female" : "Male",
                                UserName = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault().UserName ?? ""
                            }).FirstOrDefault();
            return Json(new { success = true, data = _Staff }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bouqueti
        public ActionResult Index()
        {
            var listStaff = (from a in db.Staffs
                               select new StaffDTO
                               {
                                   StaffId = a.StaffId,
                                   StaffName = a.StaffName,
                                   Dob = a.Dob,
                                   Image = a.Image,
                                   Gender = a.Gender,
                                   AccountId = a.AccountId,
                                   Position = a.Position,
                                   Phone = a.Phone,
                                   GenderName = a.Gender == true ? "Female" : "Male",
                                   UserName = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault().UserName ?? ""
                               }).ToList();
            var listCourse = (from a in db.Courses
                              select new CourseDTO
                              {
                                  CourseId = a.CourseId,
                                  CourseName = a.CourseName,
                                  Image = a.Image,
                                  Descrip = a.Descrip,
                                  Status = a.Status,
                                  CategoryId = a.CategoryId,
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                              }).ToList();
            var listTopic = (from a in db.Topics
                             select new TopicDTO
                             {
                                 TopicId = a.TopicId,
                                 TopicName = a.TopicName,
                                 Image = a.Image,
                                 Descrip = a.Descrip,
                                 Status = a.Status,
                                 CategoryId = a.CategoryId,
                                 CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                             }).ToList();
            ViewBag.ListStaff = listStaff;
            ViewBag.ListCourse = listCourse;
            ViewBag.ListTopic = listTopic;
            return View();
        }

        public ActionResult GetCourseByStaffId(int staffId)
        {
            var listId = db.StaffCourses.Where(x => x.StaffId == staffId).Select(c => c.CourseId);
            var listCourse = (from a in db.Courses.Where(x => listId.Contains(x.CourseId))
                              select new CourseDTO
                              {
                                  StaffCourseId = db.StaffCourses.Where(x => x.StaffId == staffId && x.CourseId == a.CourseId).FirstOrDefault().StaffCourseId,
                                  StaffId = staffId,
                                  CourseId = a.CourseId,
                                  CourseName = a.CourseName,
                                  Image = a.Image,
                                  Descrip = a.Descrip,
                                  Status = a.Status,
                                  CategoryId = a.CategoryId,
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                              }).ToList();
            return Json(new { success = true, data = listCourse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopicByStaffId(int staffId)
        {
            var listId = db.StaffTopics.Where(x => x.StaffId == staffId).Select(c => c.TopicId);
            var listTopic = (from a in db.Topics.Where(x => listId.Contains(x.TopicId))
                             select new TopicDTO
                             {
                                 StaffTopicId = db.StaffTopics.Where(x => x.StaffId == staffId && x.TopicId == a.TopicId).FirstOrDefault().StaffTopicId,
                                 StaffId = staffId,
                                 TopicId = a.TopicId,
                                 TopicName = a.TopicName,
                                 Image = a.Image,
                                 Descrip = a.Descrip,
                                 Status = a.Status,
                                 CategoryId = a.CategoryId,
                                 CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                             }).ToList();
            return Json(new { success = true, data = listTopic }, JsonRequestBehavior.AllowGet);
        }
    }
}