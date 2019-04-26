using System;
using System.Collections.Generic;

namespace Cookbook.Data.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RelRecipeIngredient = new HashSet<RelRecipeIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<RelRecipeIngredient> RelRecipeIngredient { get; set; }
    }
}
