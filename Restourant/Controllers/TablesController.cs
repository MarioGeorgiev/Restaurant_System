using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.Data;
using Restourant.Data.Drinks;
using Restourant.Data.Foods;
using Restourant.Data.Foods.Contracts;
using Restourant.Data.MappingTables;
using Restourant.Data.Tables;
using Restourant.Data.Tables.Contracts;
using Restourant.Models.Tables;
using System;
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
            if (productId == null)
            {
                return View(tableItems);
            }
            else
            {
                if (type == "Drinks")
                {
                    return AddDrinkToTable(id, productId, tableItems);
                }
                else if (type == "Foods")
                {
                    return AddFoodToTable(id, productId, tableItems);
                }
                return BadRequest();
            }
           
        }

        private IActionResult AddFoodToTable(int id, string productId, OrderTableViewModel tableItems)
        {
            ITable table = data.Tables.FirstOrDefault(p => p.Id == id);
            if (table == null)
            {
                return BadRequest($"Could not find table with {id}");
            }
            else
            {
                IFood food = data.Foods.FirstOrDefault(p => p.Id == productId);
                if (food == null)
                {
                    return BadRequest("No such food in the menu");
                }
                else
                {
                    var orderSameFoodMoreThenOneTime = data.TableFoods.FirstOrDefault(p => p.TableId == table.Id && p.FoodId == food.Id);
                    if (orderSameFoodMoreThenOneTime == null)
                    {
                        TableFoods tableFoods = new TableFoods();
                        tableFoods.Food = (Food)food;
                        tableFoods.Table = (Table)table;
                        table.Bill += food.Price;
                        tableFoods.OrderTimes = 1;
                        data.TableFoods.Add(tableFoods);
                        data.SaveChanges();
                    }
                    else
                    {
                        orderSameFoodMoreThenOneTime.OrderTimes++;
                        table.Bill += food.Price;
                        data.SaveChanges();
                    }


                    return RedirectToAction("Orders",tableItems.Id);
                }
            }
        }

        private IActionResult AddDrinkToTable(int id, string productId, OrderTableViewModel tableItems)
        {
            var table = data.Tables.FirstOrDefault(p => p.Id == id);
            if (table == null)
            {
                return BadRequest($"Could not find table with {id}");
            }
            else
            {
                var drink = data.Drinks.FirstOrDefault(p => p.Id == productId);
                if (drink == null)
                {
                    return BadRequest($"No such drink in the menu");
                }
                else
                {
                    var orderSameDrinkMoreThenOneTime = data.TableDrinks.FirstOrDefault(p => p.TableId == table.Id && p.DrinkId == drink.Id);
                    if (orderSameDrinkMoreThenOneTime == null)
                    {
                        TableDrinks tableDrink = new TableDrinks
                        {
                            Drink = (Drink)drink,
                            Table = (Table)table
                        };
                        table.Bill += drink.Price;
                        tableDrink.OrderTimes = 1;
                        data.TableDrinks.Add(tableDrink);
                        data.SaveChanges();
                    }
                    else
                    {
                        orderSameDrinkMoreThenOneTime.OrderTimes++;
                        table.Bill += drink.Price;
                        data.SaveChanges();

                    }
                    return RedirectToAction("Orders", tableItems.Id);
                }
            }
            
        }
    }
}
