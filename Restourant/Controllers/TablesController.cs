using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Data.Tables;
using Restourant.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Add()
        {
            return View();
        }

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
    }
}
