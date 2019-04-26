using System;
using System.Collections.Generic;

namespace Cookbook.Data.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RelRecipeIngredient = new HashSet<RelRecipeIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RelRecipeIngredient> RelRecipeIngredient { get; set; }
    }
}
