using Microsoft.AspNetCore.Mvc;
using Restourant.Data;
using Restourant.Models.Api.Foods;
using System.Collections.Generic;
using System.Linq;


namespace Restourant.Controllers.Api
{
    [ApiController]
    [Route("api/foods")]
    public class FoodsApiController : Controller
    {
        private readonly ApplicationDbContext data;

        public FoodsApiController(ApplicationDbContext data)
            => this.data = data;

        [HttpGet]
        public IEnumerable<FoodsApiModel> All()
        {
            var foods = this.data.Foods.Select(f=>new FoodsApiModel() 
            { 
                Id = f.Id,
                Name=f.Name,
                Price=f.Price,
                ServingSize = f.ServingSize
            }).ToList();
            return foods;


        }
    }
}
