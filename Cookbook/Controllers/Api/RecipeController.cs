using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Data.Dto;
using Cookbook.Data.Models;
using Cookbook.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Mvc.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IDataRepository<Recipe, RecipeDto> _dataRepository;

        public RecipeController(IDataRepository<Recipe, RecipeDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Recipe
        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            var userId = int.Parse(User.Identity.Name);
            return _dataRepository.GetAll().Where(r => r.UserId == userId);
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _dataRepository.GetDto(id);
            if (recipe == null)
            {
                return NotFound("Recipe not Found");
            }

            return Ok(recipe);
        }

        // POST: api/Recipe
        [HttpPost]
        public IActionResult Post([FromBody] Recipe recipe)
        {            
            var userId = int.Parse(User.Identity.Name);

            _dataRepository.Add(new Recipe {Name = recipe.Name, UserId = userId});
            var latestAdd = _dataRepository.GetAll().OrderByDescending(r => r.Id).FirstOrDefault();

            return RedirectToAction("Get", new {id = latestAdd?.Id});
        }

        // PUT: api/Recipe/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
