using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Data.Models;

namespace Cookbook.Data.Dto
{
    public static class Mapper
    {
        public static RecipeDto MapToDto(Recipe recipe)
        {
            return new RecipeDto()
            {
                Id = recipe.Id,
                Name = recipe.Name,

                Ingredients = recipe.RelRecipeIngredient
                    .Select(rri => 
                        new IngredientDto
                        {
                            Id = rri.Ingredient.Id,
                            Name = rri.Ingredient.Name,
                            IsAvailable = rri.IsAvailable
                        })
                    .ToList()
            };
        }
    }
}
