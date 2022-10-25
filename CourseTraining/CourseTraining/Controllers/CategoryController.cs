using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class CategoryController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Category req)
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

        public ActionResult Delete(int categoryId)
        {
            var _cate = db.Categories.Where(M => M.CategoryId == categoryId).FirstOrDefault();
            db.Categories.DeleteOnSubmit(_cate);
            db.SubmitChanges();
            return Json(new { success = true, data = _cate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int categoryId)
        {
            var _cate = db.Categories.Where(M => M.CategoryId == categoryId).FirstOrDefault();
            return Json(new { success = true, data = _cate }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var listCate = db.Categories.ToList();
            ViewBag.ListCategory = listCate;
            return View();
        }
    }
}