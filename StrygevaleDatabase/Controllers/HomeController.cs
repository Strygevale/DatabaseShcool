using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StrygevaleDatabase.Models;
using NpgsqlTypes;
using Npgsql;
using System.Data.Entity;

namespace StrygevaleDatabase.Controllers
{
    
    public class HomeController : Controller
    {
        private schoolEntities db = new schoolEntities();
      
        
        public ActionResult Query5()
        {
            var result = db.Lessons
                         .ToList();
            return View(result);
        }

      
        public ActionResult Query1()
        {
            var result = db.Lessons
                .Include(a => a.Subject)
                         .ToList();
            return View(result);
        }

        public ActionResult Query6(string txtbox6)
        {
            var result = db.Teachers
                .Where(a => a.fio.ToLower().Contains(txtbox6.ToLower()))
                         .ToList();
            return View(result);
        }
        public ActionResult Query2(string txtbox6)
        {
            var result = db.Journals
                .Include(a => a.Student)
                .Where(a => a.grade.ToLower().Contains(txtbox6.ToLower()))
                         .ToList();
            return View(result);
        }
        public ActionResult Query3()
        {
            var result = db.Lessons
                         .ToList();
            return View(result);
        }
        public ActionResult Query4(string txtbox6)
        {
            var result = db.Students
               
                .Where(a => a.fio.ToLower().Contains(txtbox6.ToLower()))
                         .ToList();
            return View(result);
        }
        public ActionResult Query9(string txtbox6)
        {
            var result = db.Lessons

                .Where(a => a.Subject.subject_name.ToLower().Contains(txtbox6.ToLower()))
                         .ToList();
            return View(result);
        }
        public ActionResult Query8()
        {
            var result = db.Journals
                         .ToList();
            return View(result);
        }
        public ActionResult Query7()
        {
            var result = db.Lessons
            
                         .ToList();
            return View(result);
        }
        public ActionResult Query10(string txtbox6)
        {
            var result = db.Lessons

                .Where(a => a.Class.class_name.ToLower().Contains(txtbox6.ToLower()))
                         .ToList();
            return View(result);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Queries()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}