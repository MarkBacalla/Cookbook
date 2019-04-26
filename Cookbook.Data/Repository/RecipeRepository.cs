using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cookbook.Data.Dto;
using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Data.Repository
{
    public class RecipeRepository : IDataRepository<Recipe, RecipeDto>
    {
        private readonly CookbookContext _cookbookContext;

        public RecipeRepository(CookbookContext cookbookContext)
        {
            _cookbookContext = cookbookContext;
        }

        public IEnumerable<Recipe> GetAll()
        {
            return _cookbookContext.Recipe.ToList();
        }

        public Recipe Get(long id)
        {            
            return _cookbookContext.Recipe.SingleOrDefault(r => r.Id == id);
        }

        public RecipeDto GetDto(long id)
        {
            var recipe = _cookbookContext.Recipe.SingleOrDefault(r => r.Id == id);

            if (recipe == null)
            {
                return null;
            }

            _cookbookContext.Entry(recipe)
                .Collection(r => r.RelRecipeIngredient)
                .Query()
                .Include(r => r.Ingredient)
                .Load();

            return Mapper.MapToDto(recipe);
        }

        public void Add(Recipe entity)
        {
            _cookbookContext.Recipe.Add(entity);
            _cookbookContext.SaveChanges();
        }

        public void Update(Recipe entityToUpdate, Recipe entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Recipe entity)
        {
            _cookbookContext.Remove(entity);
            _cookbookContext.SaveChanges();
        }

        public IEnumerable<Recipe> GetAllByUserId(int userId)
        {
            return _cookbookContext.Recipe.Where(r => r.UserId == userId).ToList();
        }
    }
}
