using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
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
        public IActionResult Orders() 
        {

            return View();
        }
    }
}
