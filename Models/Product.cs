using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;





namespace ShopApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ManufacturedBy { get; set; }

        public string Discription { get; set; }

        public int UPC { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

        public List<ProductIngredient> Ingredients { get; set; }

        public List<ProductCategory> Categories { get; set; }



        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;



    }
}