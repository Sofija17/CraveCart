using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CraveCart.Models;

namespace CraveCart.Controllers
{
    public class RestaurantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }


        public ActionResult DailyDeal()
        {
            var food = GetDailyDeal();
            if (food == null)
            {
                return HttpNotFound("No daily deal available.");
            }

            // Store the daily deal in session to access later in other views
            Session["DailyDealFoodId"] = food.Id;

            return PartialView("_DailyDeal", food);
        }


        public Food GetDailyDeal()
        {
            // Get the list of restaurant IDs
            var restaurantIds = db.Restaurants.Select(r => r.Id).ToList();

            // Check if there are any restaurants
            if (!restaurantIds.Any())
            {
                return null; // or handle the case where no restaurants exist
            }

            // Select a random restaurant ID
            Random random = new Random();
            int randomRestaurantId = restaurantIds[random.Next(restaurantIds.Count)];

            // Get a random food item from the selected restaurant
            var foodItems = db.Foods.Where(f => f.RestaurantId == randomRestaurantId).ToList();

            if (!foodItems.Any())
            {
                return null; // or handle the case where no food items exist for the selected restaurant
            }

            // Select a random food item
            var randomFood = foodItems[random.Next(foodItems.Count)];

            // Return the food item along with its associated restaurant ID
            return new Food
            {
                Name = randomFood.Name,
                Price = randomFood.Price * 0.80m, // 20% off
                ImageUrl = randomFood.ImageUrl,
                RestaurantId = randomFood.RestaurantId // Add RestaurantId here
            };
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = db.Restaurants.Include(r => r.Foods).FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return HttpNotFound();
            }

            // Retrieve the daily deal food item ID from session
            var dailyDealFoodId = Session["DailyDealFoodId"] != null ? (int?)Session["DailyDealFoodId"] : null;

            // Pass the daily deal food item ID to the view using ViewBag
            ViewBag.DailyDealFoodId = dailyDealFoodId;

            return View(restaurant);
        }


        // GET: Restaurants/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rating,Address,Phone,ImageUrl")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        public ActionResult CreateFoodItem()
        {
            var model = new FoodDto();
            ViewBag.RestaurantList = new SelectList(db.Restaurants.ToList(), "Id", "Name"); // Populate the dropdown with restaurants
            return View(model);
        }

        // POST: Restaurants/CreateFoodItem()
        [HttpPost]
        // public ActionResult CreateFoodItemm([Bind(Include = "Id,Name,Price,ImageURL,Ingredients,RestaurantId,Restaurant")] Food food)
        // {

        //   if (ModelState.IsValid)
        //  {
        //     db.Foods.Add(food);
        //    db.SaveChanges();
        //   return RedirectToAction("RestaurantMenu", new { id = food.RestaurantId }); // Redirect with RestaurantId
        // }

        // return View(food);
        // }

        public ActionResult CreateFoodItemm(FoodDto model)
        {
            if (ModelState.IsValid)
            {
                // Create a new Food instance from the DTO
                var food = new Food
                {
                    Name = model.Name,
                    Price = model.Price,
                    RestaurantId = model.RestaurantId,
                    ImageUrl = model.ImageUrl,
                    Ingredients = model.Ingredients
                };

                db.Foods.Add(food);
                db.SaveChanges();

                // Redirect back to the restaurant's details page
                return RedirectToAction("Details", new { id = food.RestaurantId });
            }

            // If not valid, reload the form with the current model state
            ViewBag.RestaurantList = new SelectList(db.Restaurants, "Id", "Name", model.RestaurantId);
            return View(model);
        }

        //Lista od fooditems 
        public ActionResult RestaurantMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Restaurant ID is missing.");
            }

            // Fetch the restaurant and include related food items
            var restaurant = db.Restaurants.Include(r => r.Foods).SingleOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return HttpNotFound("The specified restaurant could not be found.");
            }

            return View(restaurant);
        }



        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Rating,Address,Phone,ImageUrl")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        public ActionResult EditFoodItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            // Populate the restaurant dropdown
            ViewBag.RestaurantList = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);

            return View(food);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFoodItem([Bind(Include = "Id,Name,Price,Ingredients,RestaurantId,ImageUrl")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = food.RestaurantId });
            }

            // Repopulate the restaurant dropdown if the model is invalid
            ViewBag.RestaurantList = new SelectList(db.Restaurants, "Id", "Name", food.RestaurantId);
            return View(food);
        }

        // GET: Restaurants/Delete/5
        // public ActionResult Delete(int? id)
        // {
        //    if (id == null)
        //   {
        //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //  }
        //  Restaurant restaurant = db.Restaurants.Find(id);
        //  if (restaurant == null)
        //  {
        //     return HttpNotFound();
        // }
        //  return View(restaurant);
        // }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //----------------------------------------SHOPPING CART FUNCTIONALITIES START HERE----------------------------

        [Authorize]
        public ActionResult OrderSummary()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DeleteFoodItem(int id)
        {
            var food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            db.Foods.Remove(food);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
