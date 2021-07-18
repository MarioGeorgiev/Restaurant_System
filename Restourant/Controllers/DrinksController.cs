using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Drinks;
using Restourant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Add(AddDrinkViewModel drink)
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
            return RedirectPermanent("/Tables/List");
        }
    }
}
