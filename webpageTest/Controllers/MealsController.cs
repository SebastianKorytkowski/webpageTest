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
    public class MealsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Meals
        public ActionResult Index()
        {
            return View(db.GetUserMeals(User.Identity).ToList());
        }

        // GET: Meals/Create
        public ActionResult Create(DateTime? date = null)
        {
            Meal tmp = new Meal { Date = date.GetValueOrDefault(DateTime.Now) };
            return View(tmp);
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentApplicationUser = db.GetCurrentApplicationUser(User.Identity);
                if (currentApplicationUser!=null)
                {
                    meal.ApplicationUser = currentApplicationUser;
                    db.Meals.Add(meal);
                    db.SaveChanges();
                    return RedirectToAction("Edit", meal);
                }
                return View();
            }

            return View();
        }



        [HttpGet]
        public ActionResult CreateIngredient(int?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                db.IngredientsMeals.Add(new IngredientMeal{Meal = meal, Quantity = 100.0f, Ingredient = db.Ingredients.First()});
                db.SaveChanges();
            }

            return RedirectToAction("Edit", meal);
        }

        [HttpGet]
        public ActionResult DeleteIngredient(int? id)
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


            db.Entry(ingredientMeal).Reference(m => m.Meal).Load();
            Meal meal = ingredientMeal.Meal;

            db.IngredientsMeals.Remove(ingredientMeal);
            db.SaveChanges();

            return RedirectToAction("Edit", meal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIngredient(FormCollection fc)
        {
            if (Int32.TryParse(fc["item.Id"], out int id))
            {
                IngredientMeal ingredientMeal = db.IngredientsMeals.Find(id);
                if (ingredientMeal == null) return HttpNotFound();

                if (float.TryParse(fc["item.Quantity"], out float quantity)&& int.TryParse(fc["item.Ingredient"], out int ingredientId))
                {
                    Ingredient ingredient = db.Ingredients.Find(ingredientId);
                    if (ingredient == null) return HttpNotFound();

                    ingredientMeal.Quantity = quantity;
                    ingredientMeal.Ingredient = ingredient;
                    db.SaveChanges();

                    db.Entry(ingredientMeal).Reference(m=>m.Meal).Load();
                    return RedirectToAction("Edit", ingredientMeal.Meal);
                }

                db.Entry(ingredientMeal).Reference(m => m.Meal).Load();
                return RedirectToAction("Edit", ingredientMeal.Meal);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        MealViewModel CreateMealViewModel(Meal meal)
        {
            var mealIngredients = db.GetMealIngridients(meal);


            var selectList = db.Ingredients.ToList().Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    });


            return new MealViewModel{Meal = meal, AllIngridientsSelectList = selectList, MealIngredients = mealIngredients.ToList()};
        }

        // GET: Meals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(CreateMealViewModel(meal));
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MealViewModel mealViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mealViewModel.Meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CreateMealViewModel(mealViewModel.Meal));
        }

        // GET: Meals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var mealIngredients = from m in db.Meals
                    join im in db.IngredientsMeals
                        on m.Id equals im.Meal.Id
                    where m.Id == id
                    select im;

                foreach (var ingredient in mealIngredients)
                    db.IngredientsMeals.Remove(ingredient);

                Meal meal = db.Meals.Find(id);
                db.Meals.Remove(meal);
                db.SaveChanges();
                

                dbContextTransaction.Commit();
                return RedirectToAction("Index");
            }
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
