using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Drinks;
using Restourant.Models;
using System.Linq;

namespace Restourant.Controllers
{
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
                Brand = d.Brand,
                Price = d.Price,
                ServingSize = d.ServingSize
            })
             .ToList();
            return View(drinks);
        }
    }
}
