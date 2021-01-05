using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webpageTest.Models;

namespace webpageTest.Controllers
{
    [Authorize]
    public class IngredientMealsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IngredientMeals
        public ActionResult Index()
        {
            return View(db.IngredientsMeals.ToList());
        }

        // GET: IngredientMeals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMeal ingredientMeal = db.IngredientsMeals.Find(id);
            if (ingredientMeal == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMeal);
        }

        // GET: IngredientMeals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientMeals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity")] IngredientMeal ingredientMeal)
        {
            if (ModelState.IsValid)
            {
                db.IngredientsMeals.Add(ingredientMeal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingredientMeal);
        }

        // GET: IngredientMeals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMeal ingredientMeal = db.IngredientsMeals.Find(id);
            if (ingredientMeal == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMeal);
        }

        // POST: IngredientMeals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity")] IngredientMeal ingredientMeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredientMeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredientMeal);
        }

        // GET: IngredientMeals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMeal ingredientMeal = db.IngredientsMeals.Find(id);
            if (ingredientMeal == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMeal);
        }

        // POST: IngredientMeals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IngredientMeal ingredientMeal = db.IngredientsMeals.Find(id);
            db.IngredientsMeals.Remove(ingredientMeal);
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
