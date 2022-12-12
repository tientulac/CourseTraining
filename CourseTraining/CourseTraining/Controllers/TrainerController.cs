using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TrainerController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(TrainerDTO req)
        {
            if (req.TrainerId > 0)
            {
                var _trainer = db.Trainers.Where(M => M.TrainerId == req.TrainerId).FirstOrDefault();
                _trainer.TrainerName = req.TrainerName;
                _trainer.Dob = req.Dob;
                _trainer.Image = req.Image;
                _trainer.Gender = req.Gender;
                _trainer.Education = req.Education;
                _trainer.Phone = req.Phone;
                var _userUpdate = db.Accounts.Where(M => M.AccountId == _trainer.AccountId).FirstOrDefault();
                _userUpdate.Email = req.Account.Email;
                _userUpdate.Admin = req.Account.Admin;

                db.SubmitChanges();
                return Json(new { success = true, data = _trainer }, JsonRequestBehavior.AllowGet);
            }

            var _user = new Account();
            _user.UserName = req.Account.UserName;
            _user.Email = req.Account.Email;
            _user.Active = true;
            _user.Admin = req.Account.Admin;
            _user.Password = req.Account.Password;
            db.Accounts.InsertOnSubmit(_user);
            db.SubmitChanges();

            var _trainerInsert = new Trainer();
            _trainerInsert.TrainerName = req.TrainerName;
            _trainerInsert.Dob = req.Dob;
            _trainerInsert.Image = req.Image;
            _trainerInsert.Gender = req.Gender;
            _trainerInsert.AccountId = _user.AccountId;
            _trainerInsert.Education = req.Education;
            _trainerInsert.Phone = req.Phone;

            db.Trainers.InsertOnSubmit(_trainerInsert);
            db.SubmitChanges();



            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int trainerId)
        {
            var _trainer = db.Trainers.Where(M => M.TrainerId == trainerId).FirstOrDefault();
            db.Trainers.DeleteOnSubmit(_trainer);
            db.SubmitChanges();
            return Json(new { success = true, data = _trainer }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int trainerId)
        {
            var _trainer = (from a in db.Trainers.Where(x => x.TrainerId == trainerId)
                               select new TrainerDTO
                               {
                                   Account = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault(),
                                   TrainerId = a.TrainerId,
                                   TrainerName = a.TrainerName,
                                   Dob = a.Dob,
                                   Image = a.Image,
                                   Gender = a.Gender,
                                   AccountId = a.AccountId,
                                   Education = a.Education,
                                   Phone = a.Phone,
                                   GenderName = a.Gender == true ? "Female" : "Male",
                                   UserName = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault().UserName ?? ""
                               }).FirstOrDefault();
            return Json(new { success = true, data = _trainer }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bouqueti
        public ActionResult Index()
        {
            var listTrainer = (from a in db.Trainers
                               select new TrainerDTO
                               {
                                   TrainerId = a.TrainerId,
                                   TrainerName = a.TrainerName,
                                   Dob = a.Dob,
                                   Image = a.Image,
                                   Gender = a.Gender,
                                   AccountId = a.AccountId,
                                   Education = a.Education,
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
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? "",
                                  Forum = a.Forum
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
            ViewBag.ListTrainer = listTrainer;
            ViewBag.ListCourse = listCourse;
            ViewBag.ListTopic = listTopic;
            return View();
        }

        public ActionResult GetCourseByTrainerId(int trainerId)
        {
            var listId = db.TrainerCourses.Where(x => x.TrainerId == trainerId).Select(c => c.CourseId);
            var listCourse = (from a in db.Courses.Where(x => listId.Contains(x.CourseId))
                              select new CourseDTO
                              {
                                  TrainerCourseId = db.TrainerCourses.Where(x => x.TrainerId == trainerId && x.CourseId == a.CourseId).FirstOrDefault().TrainerCourseId,
                                  TrainerId = trainerId,
                                  CourseId = a.CourseId,
                                  CourseName = a.CourseName,
                                  Image = a.Image,
                                  Descrip = a.Descrip,
                                  Status = a.Status,
                                  CategoryId = a.CategoryId,
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? "",
                                  Forum = a.Forum
                              }).ToList();
            return Json(new { success = true, data = listCourse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopicByTrainerId(int trainerId)
        {
            var listId = db.TrainerTopics.Where(x => x.TrainerId == trainerId).Select(c => c.TopicId);
            var listTopic = (from a in db.Topics.Where(x => listId.Contains(x.TopicId))
                              select new TopicDTO
                              {
                                  TrainerTopicId = db.TrainerTopics.Where(x => x.TrainerId == trainerId && x.TopicId == a.TopicId).FirstOrDefault().TrainerTopicId,
                                  TrainerId = trainerId,
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

        // CourseMaster
        [HttpPost]
        public ActionResult SaveCourseMaster(Course req)
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

        public ActionResult DeleteCourseMaster(int courseId)
        {
            var _course = db.Courses.Where(M => M.CourseId == courseId).FirstOrDefault();
            db.Courses.DeleteOnSubmit(_course);
            db.SubmitChanges();
            return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindCourseMasterById(int courseId)
        {
            var _course = db.Courses.Where(M => M.CourseId == courseId).FirstOrDefault();
            return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CourseMaster()
        {
            var listCourse = (from a in db.Courses
                              select new CourseDTO
                              {
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