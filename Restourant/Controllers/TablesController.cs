using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Drinks;
using Restourant.Data.Foods;
using Restourant.Data.Foods.Contracts;
using Restourant.Data.MappingTables;
using Restourant.Data.Models.Sold;
using Restourant.Data.Sold;
using Restourant.Data.Tables;
using Restourant.Data.Tables.Contracts;
using Restourant.Infrastructure;
using Restourant.Models.Tables;
using System;
using System.Collections.Generic;
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
                IsReserved = t.IsReserved,     
                UserId = t.ApplicationUserId
            })
             .ToList();
            return View(tablesList);
        }
        [Authorize]
        public IActionResult Orders(int id,string productId, string type)
        {
            var tableItems = new OrderTableViewModel() { Id = id };
            var table = data.Tables.FirstOrDefault(p => p.Id == id);
            if (table.ApplicationUserId != User.Id() && table.ApplicationUserId!= null && !User.IsAdmin())
            {
                return Unauthorized();
            }
      
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
            var table = data.Tables.FirstOrDefault(p => p.Id == id);
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
                        table.ApplicationUserId = User.Id();
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
                        table.ApplicationUserId = User.Id();
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
        [HttpGet]
        public IActionResult Clear(int id)
        {

            ITable table = data.Tables.FirstOrDefault(p => p.Id == id);            

            table.Bill = 0;
            
         
            ICollection<TableFoods> tableFoods = data.TableFoods.Where(x => x.TableId == table.Id).ToList();
            List<FoodSold> foodSoldAlready = new List<FoodSold>();
            foreach (var food in tableFoods)
            {
                FoodSold check = data.FoodsSold.FirstOrDefault(x => x.FoodId == food.FoodId);
                if (check == null)
                {

                    FoodSold foodsSold = new FoodSold()
                    {
                        Food = data.Foods.FirstOrDefault(x => x.Id == food.FoodId),
                        DateSold = DateTime.Now,
                        ApplicationUserId = table.ApplicationUserId,
                        SoldTimes = food.OrderTimes
                    };

                    foodSoldAlready.Add(foodsSold);
                }
                else
                {
                    if (check.ApplicationUserId == User.Id())
                    {
                        check.DateSold = DateTime.Now;
                        check.SoldTimes += food.OrderTimes;
                    }
                    else
                    {
                        FoodSold foodsSold = new FoodSold()
                        {
                            Food = data.Foods.FirstOrDefault(x => x.Id == food.FoodId),
                            DateSold = DateTime.Now,
                            ApplicationUserId = table.ApplicationUserId,
                            SoldTimes = food.OrderTimes
                        };
                        foodSoldAlready.Add(foodsSold);
                    }
                    
                }

            }

            data.FoodsSold.AddRange(foodSoldAlready);
            data.RemoveRange(tableFoods);

            ICollection<TableDrinks> tableDrinks = data.TableDrinks.Where(x => x.TableId == table.Id).ToList();

            List<DrinkSold> drinksSoldAlready = new List<DrinkSold>();
            foreach (var drink in tableDrinks)
            {
                var check = data.DrinksSold.FirstOrDefault(x => x.DrinkId == drink.DrinkId);
                if (check == null)
                {
                    DrinkSold drinksSold = new DrinkSold()
                    {
                        Drink = data.Drinks.FirstOrDefault(x => x.Id == drink.DrinkId),
                        DateSold = DateTime.Now,
                        ApplicationUserId = table.ApplicationUserId,
                        SoldTime = drink.OrderTimes

                    };
                    drinksSoldAlready.Add(drinksSold);
                }
                else
                {
                    if (check.ApplicationUserId == User.Id())
                    {
                        check.DateSold = DateTime.Now;
                        check.SoldTime += drink.OrderTimes;
                    }
                    else
                    {
                        DrinkSold drinksSold = new DrinkSold()
                        {
                            Drink = data.Drinks.FirstOrDefault(x => x.Id == drink.DrinkId),
                            DateSold = DateTime.Now,
                            ApplicationUserId = table.ApplicationUserId,
                            SoldTime = drink.OrderTimes

                        };
                        drinksSoldAlready.Add(drinksSold);
                    }
                }

            }
            data.DrinksSold.AddRange(drinksSoldAlready);
            data.RemoveRange(tableDrinks);
            table.ApplicationUserId = null;
            data.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
