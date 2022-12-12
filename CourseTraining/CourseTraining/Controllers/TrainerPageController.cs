using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TrainerPageController : Controller
    {
        // GET: TrainerPage
        private LinqDataContext db = new LinqDataContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveCategory(Category req)
        {
            if (req.CategoryId > 0)
            {
                var _cate = db.Categories.Where(M => M.CategoryId == req.CategoryId).FirstOrDefault();
                _cate.CategoryName = req.CategoryName;
                _cate.CategoryCode = req.CategoryCode;
                _cate.Status = req.Status;

                db.SubmitChanges();
                return Json(new { success = true, data = _cate }, JsonRequestBehavior.AllowGet);
            }
            db.Categories.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCategory(int categoryId)
        {
            var _cate = db.Categories.Where(M => M.CategoryId == categoryId).FirstOrDefault();
            db.Categories.DeleteOnSubmit(_cate);
            db.SubmitChanges();
            return Json(new { success = true, data = _cate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindCategoryById(int categoryId)
        {
            var _cate = db.Categories.Where(M => M.CategoryId == categoryId).FirstOrDefault();
            return Json(new { success = true, data = _cate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Category()
        {
            var listCate = db.Categories.ToList();
            ViewBag.ListCategory = listCate;
            return View();
        }

        public ActionResult TrainerDetail(int id = 0)
        {
            var _Trainer = (from a in db.Trainers.Where(x => x.AccountId == id)
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
            ViewBag.Trainer = _Trainer;
            return View();
        }

        public ActionResult Course(int id = 0)
        {
            var TrainerId = db.Trainers.Where(x => x.AccountId == id).FirstOrDefault().TrainerId;
            var listId = db.TrainerCourses.Where(x => x.TrainerId == TrainerId).Select(c => c.CourseId);
            var listCourse = (from a in db.Courses.Where(x => listId.Contains(x.CourseId))
                              select new CourseDTO
                              {
                                  TrainerCourseId = db.TrainerCourses.Where(x => x.TrainerId == TrainerId && x.CourseId == a.CourseId).FirstOrDefault().TrainerCourseId,
                                  TrainerId = TrainerId,
                                  CourseId = a.CourseId,
                                  CourseName = a.CourseName,
                                  Image = a.Image,
                                  Descrip = a.Descrip,
                                  Status = a.Status,
                                  CategoryId = a.CategoryId,
                                  CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                              }).ToList();
            var listTrainerCourse = (from a in db.Courses
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
            ViewBag.TrainerId = TrainerId;
            ViewBag.ListTrainerCourse = listTrainerCourse;
            return View();
        }

        public ActionResult Topic(int id = 0)
        {
            var TrainerId = db.Trainers.Where(x => x.AccountId == id).FirstOrDefault().TrainerId;
            var listId = db.TrainerTopics.Where(x => x.TrainerId == TrainerId).Select(c => c.TopicId);
            var listTopic = (from a in db.Topics.Where(x => listId.Contains(x.TopicId))
                             select new TopicDTO
                             {
                                 TrainerTopicId = db.TrainerTopics.Where(x => x.TrainerId == TrainerId && x.TopicId == a.TopicId).FirstOrDefault().TrainerTopicId,
                                 TrainerId = TrainerId,
                                 TopicId = a.TopicId,
                                 TopicName = a.TopicName,
                                 Image = a.Image,
                                 Descrip = a.Descrip,
                                 Status = a.Status,
                                 CategoryId = a.CategoryId,
                                 CategoryName = db.Categories.Where(x => x.CategoryId == a.CategoryId).FirstOrDefault().CategoryName ?? ""
                             }).ToList();
            var listTrainerTopic = (from a in db.Topics
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
            ViewBag.TrainerId = TrainerId;
            ViewBag.ListTrainerTopic = listTrainerTopic;
            return View();
        }

        public ActionResult DeleteCourse(int courseId)
        {
            var _course = db.TrainerCourses.Where(M => M.CourseId == courseId).FirstOrDefault();
            db.TrainerCourses.DeleteOnSubmit(_course);
            db.SubmitChanges();
            return Json(new { success = true, data = _course }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTopic(int topicId)
        {
            var _topic = db.TrainerTopics.Where(M => M.TopicId == topicId).FirstOrDefault();
            db.TrainerTopics.DeleteOnSubmit(_topic);
            db.SubmitChanges();
            return Json(new { success = true, data = _topic }, JsonRequestBehavior.AllowGet);
        }

        //TRAINEE
        [HttpPost]
        public ActionResult SaveTrainee(TraineeDTO req)
        {
            if (req.TraineeId > 0)
            {
                var _trainee = db.Trainees.Where(M => M.TraineeId == req.TraineeId).FirstOrDefault();
                _trainee.TraineeName = req.TraineeName;
                _trainee.Dob = req.Dob;
                _trainee.Image = req.Image;
                _trainee.Gender = req.Gender;
                _trainee.Education = req.Education;
                _trainee.Phone = req.Phone;
                _trainee.MainProgram = req.MainProgram;
                _trainee.TOEICScore = req.TOEICScore;
                _trainee.Detail = req.Detail;
                _trainee.Address = req.Address;

                db.SubmitChanges();
                return Json(new { success = true, data = _trainee }, JsonRequestBehavior.AllowGet);
            }

            var _user = new Account();
            _user.UserName = req.Account.UserName;
            _user.Email = req.Account.Email;
            _user.Active = true;
            _user.Admin = req.Account.Admin;
            _user.Password = req.Account.Password;
            db.Accounts.InsertOnSubmit(_user);
            db.SubmitChanges();

            var _traineeInsert = new Trainee();
            _traineeInsert.AccountId = _user.AccountId;
            _traineeInsert.TraineeName = req.TraineeName;
            _traineeInsert.Dob = req.Dob;
            _traineeInsert.Image = req.Image;
            _traineeInsert.Gender = req.Gender;
            _traineeInsert.Education = req.Education;
            _traineeInsert.Phone = req.Phone;
            _traineeInsert.MainProgram = req.MainProgram;
            _traineeInsert.TOEICScore = req.TOEICScore;
            _traineeInsert.Detail = req.Detail;
            _traineeInsert.Address = req.Address;

            db.Trainees.InsertOnSubmit(_traineeInsert);
            db.SubmitChanges();



            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteTrainee(int traineeId)
        {
            var _trainee = db.Trainees.Where(M => M.TraineeId == traineeId).FirstOrDefault();
            db.Trainees.DeleteOnSubmit(_trainee);
            db.SubmitChanges();
            return Json(new { success = true, data = _trainee }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindTraineeById(int traineeId)
        {
            var _trainee = (from a in db.Trainees.Where(x => x.TraineeId == traineeId)
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
            return Json(new { success = true, data = _trainee }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bouqueti
        public ActionResult Trainee()
        {
            var listTrainee = (from a in db.Trainees
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
            ViewBag.ListTrainee = listTrainee;
            ViewBag.ListCourse = listCourse;
            ViewBag.ListTopic = listTopic;
            return View();
        }

        public ActionResult GetCourseByTraineeId(int traineeId)
        {
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
            return Json(new { success = true, data = listCourse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopicByTraineeId(int traineeId)
        {
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
            return Json(new { success = true, data = listTopic }, JsonRequestBehavior.AllowGet);
        }
    }
}