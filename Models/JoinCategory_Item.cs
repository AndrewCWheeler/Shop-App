using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class ItemCategory
    {
        [Key]
        public int ItemCategoryId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}