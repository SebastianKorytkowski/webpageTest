using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using webpageTest.Models;

namespace webpageTest.Controllers
{
    [Authorize]
    public class ExcercisesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Excercises
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var excercises = from e in db.Excercises
                join u in db.Users
                    on e.ApplicationUser equals u
                where u.Id == userId
                select e;

            return View(excercises.ToList());
        }

        // GET: Excercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Excercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time")] Excercise excercise)
        {
            if (ModelState.IsValid)
            {
                excercise.ApplicationUser = db.GetCurrentApplicationUser(User.Identity);
                db.Excercises.Add(excercise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit", CreateExcerciseViewModel(excercise));
        }

        ExcerciseViewModel CreateExcerciseViewModel(Excercise excercise)
        {
            var selectList = db.ExcerciseTypes.ToList().Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

            return new ExcerciseViewModel { Excercise = excercise, AllExcerciseTypeSelectList = selectList};
        }

        // GET: Excercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Excercise excercise = db.Excercises.Find(id);
            if (excercise == null)
            {
                return HttpNotFound();
            }
            return View(CreateExcerciseViewModel(excercise));
        }

        // POST: Excercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time")] Excercise excercise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(excercise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CreateExcerciseViewModel(excercise));
        }

        // GET: Excercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Excercise excercise = db.Excercises.Find(id);
            if (excercise == null)
            {
                return HttpNotFound();
            }
            return View(excercise);
        }

        // POST: Excercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Excercise excercise = db.Excercises.Find(id);
            db.Excercises.Remove(excercise);
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
