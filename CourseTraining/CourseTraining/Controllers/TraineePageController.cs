using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TraineePageController : Controller
    {
        // GET: TraineePage
        private LinqDataContext db = new LinqDataContext();

        public ActionResult Index()
        {
            //var _trainee = (from a in db.Trainees.Where(x => x.AccountId == id)
            //                select new TraineeDTO
            //                {
            //                    Account = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault(),
            //                    TraineeId = a.TraineeId,
            //                    TraineeName = a.TraineeName,
            //                    Dob = a.Dob,
            //                    Image = a.Image,
            //                    Gender = a.Gender,
            //                    AccountId = a.AccountId,
            //                    Education = a.Education,
            //                    Phone = a.Phone,
            //                    GenderName = a.Gender == true ? "Female" : "Male",
            //                    UserName = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault().UserName ?? "",
            //                    MainProgram = a.MainProgram,
            //                    TOEICScore = a.TOEICScore,
            //                    Detail = a.Detail,
            //                    Address = a.Address
            //                }).FirstOrDefault();
            //ViewBag.Trainee = _trainee;
            return View();
        }

        public ActionResult TraineeDetail(int id = 0)
        {
            var _trainee = (from a in db.Trainees.Where(x => x.AccountId == id)
                            select new TraineeDTO
                            {
                                Account = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault(),
                                TraineeId = a.TraineeId,
                                TraineeName = a.TraineeName,
                                Dob = a.Dob,
                                Image = a.Image,
                                Gender = a.Gender,
                                AccountId = a.AccountId,
                                Education = a.Education,
                                Phone = a.Phone,
                                GenderName = a.Gender == true ? "Female" : "Male",
                                UserName = db.Accounts.Where(x => x.AccountId == a.AccountId).FirstOrDefault().UserName ?? "",
                                MainProgram = a.MainProgram,
                                TOEICScore = a.TOEICScore,
                                Detail = a.Detail,
                                Address = a.Address
                            }).FirstOrDefault();
            ViewBag.Trainee = _trainee;
            return View();
        }

        public ActionResult Course(int id = 0)
        {
            var traineeId = db.Trainees.Where(x => x.AccountId == id).FirstOrDefault().TraineeId;
            var listId = db.TraineeCourses.Where(x => x.TraineeId == traineeId).Select(c => c.CourseId);
            var listCourse = (from a in db.Courses.Where(x => listId.Contains(x.CourseId))
                              select new CourseDTO
                              {
                                  TraineeCourseId = db.TraineeCourses.Where(x => x.TraineeId == traineeId && x.CourseId == a.CourseId).FirstOrDefault().TraineeCourseId,
                                  TraineeId = traineeId,
                                  CourseId = a.CourseId,
                                  CourseName = a.CourseName,
                                  Image = a.Image,
                                  Descrip = a.Descrip,
                                  Status = a.Status,
                                  CategoryId = a.CategoryId,
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                              }).ToList();
            var listTraineeCourse = (from a in db.Courses
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
            ViewBag.ListCourse = listCourse;
            ViewBag.TraineeId = traineeId;
            ViewBag.ListTraineeCourse = listTraineeCourse;
            return View();
        }

        public ActionResult Topic(int id = 0)
        {
            var traineeId = db.Trainees.Where(x => x.AccountId == id).FirstOrDefault().TraineeId;
            var listId = db.TraineeTopics.Where(x => x.TraineeId == traineeId).Select(c => c.TopicId);
            var listTopic = (from a in db.Topics.Where(x => listId.Contains(x.TopicId))
                             select new TopicDTO
                             {
                                 TraineeTopicId = db.TraineeTopics.Where(x => x.TraineeId == traineeId && x.TopicId == a.TopicId).FirstOrDefault().TraineeTopicId,
                                 TraineeId = traineeId,
                                 TopicId = a.TopicId,
                                 TopicName = a.TopicName,
                                 Image = a.Image,
                                 Descrip = a.Descrip,
                                 Status = a.Status,
                                 CategoryId = a.CategoryId,
                                 CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                             }).ToList();
            var listTraineeTopic = (from a in db.Topics
                             select new TopicDTO
                             {
                                 TopicId = a.TopicId,
                                 TopicName = a.TopicName,
                                 Image = a.Image,
                                 Descrip = a.Descrip,
                                 Status = a.Status,
                                 CategoryId = a.CategoryId,
                                 CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? "",
                                 Author = a.Author
                             }).ToList();
            ViewBag.ListTopic = listTopic;
            ViewBag.TraineeId = traineeId;
            ViewBag.ListTraineeTopic = listTraineeTopic;
            return View();
        }

        public ActionResult DeleteCourse(int courseId)
        {
            var _course = db.TraineeCourses.Where(M => M.CourseId == courseId).FirstOrDefault();
            db.TraineeCourses.DeleteOnSubmit(_course);
            db.SubmitChanges();
            return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTopic(int topicId)
        {
            var _topic = db.TraineeTopics.Where(M => M.TopicId == topicId).FirstOrDefault();
            db.TraineeTopics.DeleteOnSubmit(_topic);
            db.SubmitChanges();
            return Json(new { success = true, data = _topic }, JsonRequestBehavior.AllowGet);
        }
    }
}