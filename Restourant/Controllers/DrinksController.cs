using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Drinks;
using Restourant.Models;
using System.Linq;

namespace Restourant.Controllers
{
    [Authorize(Roles ="admin")]
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext data;

        public DrinksController(ApplicationDbContext data)
            => this.data = data;

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(DrinkViewModel drink)
        {
            var drinkToAdd = new Drink()
            {
                Name = drink.Name,
                Brand = drink.Brand,
                Price = drink.Price,
                ServingSize = drink.ServingSize
            };
            this.data.Add(drinkToAdd);
            this.data.SaveChanges();
            return RedirectPermanent("/Drinks/List");
        }

        public IActionResult List()
        {
            var drinks = this.data.Drinks.Select(d => new DrinkViewModel
            {

                Name = d.Name,
                Id = d.Id,
                Brand = d.Brand,
                Price = d.Price,
                ServingSize = d.ServingSize
            })
             .ToList();
            return View(drinks);
        }

        public IActionResult Delete(string id) 
        {
            var drinkToRemove = this.data.Drinks.FirstOrDefault(d => d.Id == id);
            if (drinkToRemove == null)
            {
                return BadRequest();
            }
            data.Remove(drinkToRemove);
            data.SaveChanges();
            return RedirectPermanent("/Drinks/List");
        }


        public IActionResult Edit(string id)
        {
            var drinkToEdit = this.data.Drinks.FirstOrDefault(d => d.Id == id);
            if (drinkToEdit == null)
            {
                return BadRequest();
            }
            return View(new DrinkViewModel
            {
                Name = drinkToEdit.Name,
                Id = drinkToEdit.Id,
                Brand = drinkToEdit.Brand,
                Price = drinkToEdit.Price,
                ServingSize = drinkToEdit.ServingSize
            });
        }

        [HttpPost]

        public IActionResult Edit(string id, DrinkViewModel drink)
        {
            if (!ModelState.IsValid)
            {

                return View(drink);
            }
            var drinkToEdit = data.Drinks.FirstOrDefault(d => d.Id == id);
            if (drinkToEdit == null)
            {
                return BadRequest();
            }


            drinkToEdit.Name = drink.Name;
            drinkToEdit.Brand = drink.Brand;
            drinkToEdit.ServingSize = drink.ServingSize;
            drinkToEdit.Price = drink.Price;
            this.data.SaveChanges();

            return RedirectToAction(nameof(List));
        }

    }
}
