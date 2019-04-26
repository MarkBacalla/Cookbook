using System.Collections.Generic;
using Cookbook.Data.Models;

namespace Cookbook.Data.Repository
{
    public interface IDataRepository<TEntity, TDto>    
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        TDto GetDto(long id);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }

    public interface IIngredientAvailabilty
    {
        void Update(int recipeId, int ingredientId, bool isAvailable);
        void ClearAll(int recipeId);
    }
}
