using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StrygevaleDatabase.Models;

namespace StrygevaleDatabase.Controllers
{
    public class LessonsController : Controller
    {
        private schoolEntities db = new schoolEntities();

        // GET: Lessons
        public ActionResult Index()
        {
            var lessons = db.Lessons.Include(l => l.Class).Include(l => l.Subject).Include(l => l.Teacher);
            return View(lessons.ToList());
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Search(string search)
        {

            var result = db.Lessons

              .Where(a => a.Class.class_name.ToLower().Contains(search.ToLower()) || a.Subject.subject_name.ToLower().Contains(search.ToLower()) || a.Teacher.fio.ToLower().Contains(search.ToLower()))
              .ToList();

            return View(result);
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }
       


        // GET: Lessons/Create
        public ActionResult Create()
        {
            ViewBag.id_class = new SelectList(db.Classes, "id_class", "class_name");
            ViewBag.id_subject = new SelectList(db.Subjects, "id_subject", "subject_name");
            ViewBag.id_teacher = new SelectList(db.Teachers, "id_teacher", "fio");
            return View();
        }

        // POST: Lessons/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_lesson,id_subject,id_class,id_teacher,data")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_class = new SelectList(db.Classes, "id_class", "class_name", lesson.id_class);
            ViewBag.id_subject = new SelectList(db.Subjects, "id_subject", "subject_name", lesson.id_subject);
            ViewBag.id_teacher = new SelectList(db.Teachers, "id_teacher", "fio", lesson.id_teacher);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_class = new SelectList(db.Classes, "id_class", "class_name", lesson.id_class);
            ViewBag.id_subject = new SelectList(db.Subjects, "id_subject", "subject_name", lesson.id_subject);
            ViewBag.id_teacher = new SelectList(db.Teachers, "id_teacher", "fio", lesson.id_teacher);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_lesson,id_subject,id_class,id_teacher,data")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_class = new SelectList(db.Classes, "id_class", "class_name", lesson.id_class);
            ViewBag.id_subject = new SelectList(db.Subjects, "id_subject", "subject_name", lesson.id_subject);
            ViewBag.id_teacher = new SelectList(db.Teachers, "id_teacher", "fio", lesson.id_teacher);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lesson lesson = db.Lessons.Find(id);
            db.Lessons.Remove(lesson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
