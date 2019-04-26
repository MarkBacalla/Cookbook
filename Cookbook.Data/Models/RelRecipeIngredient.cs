using System;
using System.Collections.Generic;

namespace Cookbook.Data.Models
{
    public partial class RelRecipeIngredient
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public bool IsAvailable { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
