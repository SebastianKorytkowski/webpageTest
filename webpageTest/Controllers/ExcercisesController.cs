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
            return View(db.GetUserExercises(User.Identity).ToList());
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
        public ActionResult Create([Bind(Include = "Id,Date")] Excercise excercise)
        {
            if (ModelState.IsValid)
            {
                excercise.ApplicationUser = db.GetCurrentApplicationUser(User.Identity);
                excercise.ExType = db.ExcerciseTypes.First();
                db.Excercises.Add(excercise);
                db.SaveChanges();

                return RedirectToAction("Edit", excercise);
            }

            return RedirectToAction("Index");
        }

        ExcerciseViewModel CreateExcerciseViewModel(Excercise excercise)
        {
            var selectList = db.ExcerciseTypes.ToList().Select(x =>
                new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });

            //Make sure the coresponding execisetype is loaded
            db.Entry(excercise).Reference(e => e.ExType).Load();

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
        public ActionResult Edit(FormCollection fc)
        {
            if (Int32.TryParse(fc["Excercise.Id"], out int id))
            {
                Excercise excercise = db.Excercises.Find(id);
                if (excercise == null) return HttpNotFound();
                if (float.TryParse(fc["Excercise.Length"], out float length) && int.TryParse(fc["Excercise.ExType.Id"], out int excerciseTypeId) && DateTime.TryParse(fc["Excercise.Date"], out DateTime date))
                {
                    ExcerciseType excerciseType = db.ExcerciseTypes.Find(excerciseTypeId);
                    if (excerciseType == null) return HttpNotFound();
                    excercise.ExType = excerciseType;
                    excercise.Length = length;
                    excercise.Date = date;
                    db.SaveChanges();

                    db.Entry(excercise).Reference(m => m.ExType).Load();
                    return RedirectToAction("Edit", excercise);
                }
            }


            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIngredient(FormCollection fc)
        {
            if (Int32.TryParse(fc["item.Id"], out int id))
            {
                IngredientMeal ingredientMeal = db.IngredientsMeals.Find(id);
                if (ingredientMeal == null) return HttpNotFound();

                if (float.TryParse(fc["item.Quantity"], out float quantity) && int.TryParse(fc["item.Ingredient"], out int ingredientId))
                {
                    Ingredient ingredient = db.Ingredients.Find(ingredientId);
                    if (ingredient == null) return HttpNotFound();

                    ingredientMeal.Quantity = quantity;
                    ingredientMeal.Ingredient = ingredient;
                    db.SaveChanges();

                    db.Entry(ingredientMeal).Reference(m => m.Meal).Load();
                    return RedirectToAction("Edit", ingredientMeal.Meal);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
