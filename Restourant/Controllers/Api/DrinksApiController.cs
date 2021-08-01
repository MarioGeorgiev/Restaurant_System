using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Models.Api.Drinks;
using System.Collections.Generic;
using System.Linq;

namespace Restourant.Controllers.Api
{
    [ApiController]
    [Route("api/drinks")]
    public class DrinksApiController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public DrinksApiController(ApplicationDbContext data)
            => this.data = data;

        [HttpGet]
        public IEnumerable<DrinksApiModel> All() 
        {
            var drinks = this.data.Drinks.Select(d => new DrinksApiModel() 
            { 
                Id = d.Id,
                Name= d.Name,
                Brand = d.Brand,
                Price =d.Price,
                ServingSize =d.ServingSize
            })
                .ToList();
            return drinks;
 
        }
    }
}
