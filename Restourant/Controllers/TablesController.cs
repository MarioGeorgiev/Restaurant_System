using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Drinks;
using Restourant.Data.MappingTables;
using Restourant.Data.Tables;
using Restourant.Models.Tables;
using System.Linq;


namespace Restourant.Controllers
{
    public class TablesController : Controller
    {
        private readonly ApplicationDbContext data;

        public TablesController(ApplicationDbContext data)
            => this.data = data;
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Add(AddTableViewModel table)
        {
            var tableToAdd = new Table()
            {
                Capacity = table.Capacity
            };
            this.data.Add(tableToAdd);
            this.data.SaveChanges();

            return RedirectPermanent("/Tables/List");
        }
        [Authorize]
        public IActionResult List(ListTablesViewModel tables)
        {
            var tablesList = this.data.Tables.Select(t => new ListTablesViewModel
            {
                Id = t.Id,
                Capacity = t.Capacity,
                IsReserved = t.IsReserved
            })
             .ToList();
            return View(tablesList);
        }
        [Authorize]
        public IActionResult Orders(int id,string productId, string type)
        {
            var tableItems = new OrderTableViewModel() { Id = id };
            System.Console.WriteLine($"{id}:{productId}:{type}");
            //var table = data.Tables.FirstOrDefault(p => p.Id == id);
            //if (table == null)
            //{
            //    return BadRequest($"Could not find table with {id}");
            //}
            //else
            //{
            //    var drink = data.Drinks.FirstOrDefault(p => p.Id == "9c7895fd-8aac-42b0-a613-fe27c7982afc");
            //    if (drink == null)
            //    {
            //        return BadRequest($"No {drink.Name} in the menu");
            //    }
            //    else
            //    {
            //        var orderSameDrinkMoreThenOneTime = data.TableDrinks.FirstOrDefault(p => p.TableId == table.Id && p.DrinkId == drink.Id);
            //        if (orderSameDrinkMoreThenOneTime == null)
            //        {
            //            TableDrinks tableDrink = new TableDrinks
            //            {
            //                Drink = (Drink)drink,
            //                Table = (Table)table
            //            };
            //            table.Bill += drink.Price;
            //            tableDrink.OrderTimes = 1;
            //            data.TableDrinks.Add(tableDrink);
            //            data.SaveChanges();
            //            return View();
            //        }
            //        else
            //        {
            //            orderSameDrinkMoreThenOneTime.OrderTimes++;
            //            table.Bill += drink.Price;
            //            data.SaveChanges();
            //            return View();
            //        }



            //    }
            //}
            if (productId == null)
            {
                return View(tableItems);
            }
            else
            {
                return BadRequest($"{id}:{productId}:{type}");
            }
           
        }

    }
}
