using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.Data;
using Restourant.Models.Api.Drinks;
using Restourant.Models.Api.Foods;
using Restourant.Models.Api.TableOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Controllers.Api
{
    [Route("api/table")]
    [ApiController]
    public class TableOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public TableOrdersController(ApplicationDbContext data)
            => this.data = data;

        [HttpGet]
        public TableOrdersApiModel All(int id)
        {
            Console.WriteLine(id);
            var table = data.Tables
                .Include(x => x.FoodOrders).ThenInclude(x => x.Food)
                .Include(x => x.DrinkOrders).ThenInclude(x => x.Drink)
                .FirstOrDefault(x => x.Id == id);

            var drinks = table.DrinkOrders.Select(d => new DrinksApiModel()
            {
                        Id = d.Drink.Id,
                        Name = d.Drink.Name,
                        ServingSize = d.Drink.ServingSize,
                        Brand = d.Drink.Brand,
                        Price = d.Drink.Price
            });

            var foods = table.FoodOrders.Select(f => new FoodsApiModel()
            {
                Id = f.Food.Id,
                Name = f.Food.Name,
                ServingSize = f.Food.ServingSize,               
                Price = f.Food.Price

            });

            decimal bill = table.Bill;

            TableOrdersApiModel orders= new TableOrdersApiModel()
            {
                DrinksOrdered = drinks,
                FoodsOrdered = foods,
                Bill = bill
            };

            return orders;
        }
    }
}
