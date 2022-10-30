using CourseTraining.Models;
using CourseTraining.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TopicController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Topic req)
        {
            if (req.TopicId > 0)
            {
                var _topic = db.Topics.Where(M => M.TopicId == req.TopicId).FirstOrDefault();
                _topic.TopicName = req.TopicName;
                _topic.Image = req.Image;
                _topic.Descrip = req.Descrip;
                _topic.Status = req.Status;
                _topic.CategoryId = req.CategoryId;
                _topic.Author = req.Author;

                db.SubmitChanges();
                return Json(new { success = true, data = _topic }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                req.Status = 1;
            }
            db.Topics.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int topicId)
        {
            var _topic = db.Topics.Where(M => M.TopicId == topicId).FirstOrDefault();
            db.Topics.DeleteOnSubmit(_topic);
            db.SubmitChanges();
            return Json(new { success = true, data = _topic }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int topicId)
        {
            var _topic = db.Topics.Where(M => M.TopicId == topicId).FirstOrDefault();
            return Json(new { success = true, data = _topic }, JsonRequestBehavior.AllowGet);
        }

        // GET: Bouqueti
        public ActionResult Index()
        {
            var listTopic = (from a in db.Topics
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
            ViewBag.ListCategory = db.Categories.ToList();
            return View();
        }
    }
}