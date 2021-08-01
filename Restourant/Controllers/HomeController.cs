using Microsoft.AspNetCore.Mvc;
using Restourant.Models;
using System.Diagnostics;

namespace Restourant.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            System.Console.WriteLine(User.Identity.IsAuthenticated);
            if (User.Identity.IsAuthenticated)
            {
                return RedirectPermanent("Tables/List");
            }
            else
            {
                return RedirectPermanent("/Identity/Account/Login");
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
