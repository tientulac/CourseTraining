using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TraineeController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(TraineeDTO req)
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

        public ActionResult Delete(int traineeId)
        {
            var _trainee = db.Trainees.Where(M => M.TraineeId == traineeId).FirstOrDefault();
            db.Trainees.DeleteOnSubmit(_trainee);
            db.SubmitChanges();
            return Json(new { success = true, data = _trainee }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int traineeId)
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
        public ActionResult Index()
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
            ViewBag.ListTrainee = listTrainee;
            return View();
        }
    }
}