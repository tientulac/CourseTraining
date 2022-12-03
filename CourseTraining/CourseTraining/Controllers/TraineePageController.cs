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

        public ActionResult Index(int id = 0)
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

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult Topic()
        {
            return View();
        }
    }
}