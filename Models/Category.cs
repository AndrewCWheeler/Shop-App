using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;





namespace ShopApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Discription { get; set; }

        public List<ProductCategory> Products { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;



    }

}