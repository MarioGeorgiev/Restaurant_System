using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Foods;
using Restourant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Controllers
{
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
        public IActionResult Add(AddFoodViewModel food)
        {
            var drinkToAdd = new Food()
            {
                Name = food.Name,               
                Price = food.Price,
                ServingSize = food.ServingSize
            };
            this.data.Add(drinkToAdd);
            this.data.SaveChanges();
            return RedirectPermanent("/Tables/List");
        }
    }
}
