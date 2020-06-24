using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;





namespace ShopApp.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string Discription { get; set; }

        public List<ProductIngredient> Products { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}