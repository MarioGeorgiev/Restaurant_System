using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Foods;
using Restourant.Models;
using System.Linq;

namespace Restourant.Controllers
{
    [Authorize(Roles = "admin")]
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext data;

        public FoodsController(ApplicationDbContext data)
            => this.data = data;
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(FoodViewModel food)
        {
            var drinkToAdd = new Food()
            {
                Name = food.Name,
                Price = food.Price,
                ServingSize = food.ServingSize
            };
            this.data.Add(drinkToAdd);
            this.data.SaveChanges();
            return RedirectPermanent("/Foods/List");
        }

        public IActionResult List()
        {
            var foods = this.data.Foods.Select(d => new FoodViewModel
            {
                Name = d.Name,
                Id = d.Id,
                Price = d.Price,
                ServingSize = d.ServingSize
            })
            .ToList();
            return View(foods);
        }

        public IActionResult Delete(string id)
        {
            var foodToRemove = this.data.Foods.FirstOrDefault(d => d.Id == id);
            if (foodToRemove == null)
            {
                return BadRequest();
            }
            data.Remove(foodToRemove);
            data.SaveChanges();
            return RedirectPermanent("/Foods/List");
        }

        public IActionResult Edit(string id)
        {
            var foodkToEdit = this.data.Foods.FirstOrDefault(d => d.Id == id);
            if (foodkToEdit == null)
            {
                return BadRequest();
            }
            return View(new FoodViewModel
            {
                Name = foodkToEdit.Name,
                Id = foodkToEdit.Id,                
                Price = foodkToEdit.Price,
                ServingSize = foodkToEdit.ServingSize
            });
        }

        [HttpPost]
        public IActionResult Edit(string id, FoodViewModel food)
        {
            if (!ModelState.IsValid)
            {

                return View(food);
            }
            var foodToEdit = data.Foods.FirstOrDefault(d => d.Id == id);
            if (foodToEdit == null)
            {
                return BadRequest();
            }

            foodToEdit.Name = food.Name;
            foodToEdit.ServingSize = food.ServingSize;
            foodToEdit.Price = food.Price;
            this.data.SaveChanges();
            
            return RedirectToAction(nameof(List));
        }
    }
}
