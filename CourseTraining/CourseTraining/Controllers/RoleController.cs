using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class RoleController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Role req)
        {
            if (req.RoleId > 0)
            {
                var _role = db.Roles.Where(M => M.RoleId == req.RoleId).FirstOrDefault();
                _role.RoleCode = req.RoleCode;
                _role.RoleName = req.RoleName;

                db.SubmitChanges();
                return Json(new { success = true, data = _role }, JsonRequestBehavior.AllowGet);
            }
            db.Roles.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int roleId)
        {
            var _role = db.Roles.Where(M => M.RoleId == roleId).FirstOrDefault();
            db.Roles.DeleteOnSubmit(_role);
            db.SubmitChanges();
            return Json(new { success = true, data = _role }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int roleId)
        {
            var _role = db.Roles.Where(M => M.RoleId == roleId).FirstOrDefault();
            return Json(new { success = true, data = _role }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var listRole = db.Roles.ToList();
            ViewBag.ListRole = listRole;
            return View();
        }
    }
}