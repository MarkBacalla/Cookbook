using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Data.Dto;
using Cookbook.Data.Models;
using Cookbook.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IngredientController : ControllerBase
    {
        private readonly IDataRepository<Ingredient, IngredientDto> _dataRepository;

        public IngredientController(IDataRepository<Ingredient, IngredientDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }
       
        // POST: api/Ingredient/ClearAvailability/1
        [HttpPost("{recipeId}")]
        [Route("[action]/{recipeId}")]
        public IActionResult ClearAvailability(int recipeId)
        {
            (_dataRepository as IIngredientAvailabilty)?.ClearAll(recipeId);
            
            return Ok();
        }

        // POST: api/Ingredient
        [HttpPost]
        public void Post([FromBody] AddIngredientRequest ingredientRequest)
        {            
            var newIngredient = new Ingredient {Name = ingredientRequest.Name};
            newIngredient.RelRecipeIngredient.Add(new RelRecipeIngredient { RecipeId = ingredientRequest.RecipeId });            

            _dataRepository.Add(newIngredient);
        }

        // PUT: api/Ingredient
        [HttpPut]
        public void Put([FromBody] UpdateIngredientAvailabilityRequest request)
        {
            var statusUpdateRepo = _dataRepository as IIngredientAvailabilty;
            statusUpdateRepo?.Update(request.RecipeId, request.IngredientId, request.IsAvailable);
        }
    }

    public class AddIngredientRequest
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
    }

    public class UpdateIngredientAvailabilityRequest
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
