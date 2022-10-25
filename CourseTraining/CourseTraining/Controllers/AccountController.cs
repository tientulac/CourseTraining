using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class AccountController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Account req)
        {
            if (req.AccountId > 0)
            {
                var _account = db.Accounts.Where(M => M.AccountId == req.AccountId).FirstOrDefault();
                _account.Email = req.Email;
                _account.Active = req.Active;
                _account.Admin = req.Admin;

                db.SubmitChanges();
                return Json(new { success = true, data = _account }, JsonRequestBehavior.AllowGet);
            }
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int accountId)
        {
            var _account = db.Accounts.Where(M => M.AccountId == accountId).FirstOrDefault();
            db.Accounts.DeleteOnSubmit(_account);
            db.SubmitChanges();
            return Json(new { success = true, data = _account }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int accountId)
        {
            var _account = db.Accounts.Where(M => M.AccountId == accountId).FirstOrDefault();
            return Json(new { success = true, data = _account }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            ViewBag.ListAccount = db.Accounts.ToList();
            return View();
        }
    }
}