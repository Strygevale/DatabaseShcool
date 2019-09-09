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
    public class JournalsController : Controller
    {
        private schoolEntities db = new schoolEntities();

        // GET: Journals
        public ActionResult Index()
        {
            var journals = db.Journals.Include(j => j.Lesson).Include(j => j.Student);
            return View(journals.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string search)
        {

            var result = db.Journals

              .Where(a => a.grade.ToLower().Contains(search.ToLower()) || a.Student.fio.ToLower().Contains(search.ToLower()))
              .ToList();

            return View(result);
        }

        // GET: Journals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }
      
       

        // GET: Journals/Create
        public ActionResult Create()
        {
            ViewBag.id_lesson = new SelectList(db.Lessons, "id_lesson", "id_lesson");
            ViewBag.id_student = new SelectList(db.Students, "id_student", "fio");
            return View();
        }

        // POST: Journals/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_journal,id_lesson,grade,id_student")] Journal journal)
        {
            if (ModelState.IsValid)
            {
                db.Journals.Add(journal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_lesson = new SelectList(db.Lessons, "id_lesson", "id_lesson", journal.id_lesson);
            ViewBag.id_student = new SelectList(db.Students, "id_student", "fio", journal.id_student);
            return View(journal);
        }

        // GET: Journals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_lesson = new SelectList(db.Lessons, "id_lesson", "id_lesson", journal.id_lesson);
            ViewBag.id_student = new SelectList(db.Students, "id_student", "fio", journal.id_student);
            return View(journal);
        }

        // POST: Journals/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_journal,id_lesson,grade,id_student")] Journal journal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_lesson = new SelectList(db.Lessons, "id_lesson", "id_lesson", journal.id_lesson);
            ViewBag.id_student = new SelectList(db.Students, "id_student", "fio", journal.id_student);
            return View(journal);
        }

        // GET: Journals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journal journal = db.Journals.Find(id);
            db.Journals.Remove(journal);
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
