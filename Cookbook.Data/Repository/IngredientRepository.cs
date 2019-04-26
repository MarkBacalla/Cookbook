using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cookbook.Data.Dto;
using Cookbook.Data.Models;

namespace Cookbook.Data.Repository
{
    public class IngredientRepository : IDataRepository<Ingredient, IngredientDto>, IIngredientAvailabilty
    {
        private readonly CookbookContext _cookbookContext;

        public IngredientRepository(CookbookContext cookbookContext)
        {
            _cookbookContext = cookbookContext;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _cookbookContext.Ingredient.ToList();
        }

        public Ingredient Get(long id)
        {
            return _cookbookContext.Ingredient.SingleOrDefault();
        }

        public IngredientDto GetDto(long id)
        {
            throw new NotImplementedException();
        }

        public void Add(Ingredient entityToAdd)
        {
            var recipe =
                _cookbookContext.Recipe.SingleOrDefault(r => r.Id == entityToAdd.RelRecipeIngredient.First().RecipeId);
            if (recipe == null)
                throw new InvalidOperationException();

            // check if Ingredient already exists
            var ingredient = _cookbookContext.Ingredient.SingleOrDefault(i => i.Name == entityToAdd.Name);
            if (ingredient == null)
            {
                ingredient = new Ingredient {Name = entityToAdd.Name};
                _cookbookContext.Ingredient.Add(ingredient);
                _cookbookContext.SaveChanges();
            }

            // save RelRecipeIngredient
            _cookbookContext.RelRecipeIngredient.Add(new RelRecipeIngredient { RecipeId = recipe.Id, IngredientId = ingredient.Id });
            _cookbookContext.SaveChanges();

        }

        public void Update(Ingredient entityToUpdate, Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ingredient entity)
        {
            _cookbookContext.Remove(entity);
            _cookbookContext.SaveChanges();
        }

        public void Update(int recipeId, int ingredientId, bool isAvailable)
        {
            var recipeIngredient = _cookbookContext.RelRecipeIngredient.SingleOrDefault(rri =>
                rri.RecipeId == recipeId && rri.IngredientId == ingredientId);

            if (recipeIngredient == null)
                throw new ArgumentException("Invalid ingredient or recipe");

            recipeIngredient.IsAvailable = isAvailable;
            _cookbookContext.SaveChanges();
        }

        public void ClearAll(int recipeId)
        {
            var recipeIngredients = _cookbookContext.RelRecipeIngredient.Where(rri => rri.RecipeId == recipeId);
            foreach (var rri in recipeIngredients)
            {
                rri.IsAvailable = false;
            }

            _cookbookContext.SaveChanges();
        }
    }
}
