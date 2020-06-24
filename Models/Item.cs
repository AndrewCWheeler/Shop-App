using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;





namespace ShopApp.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public int Aisle { get; set; }

        public int Row { get; set; }

        public int Shelf { get; set; }

        public List<Price> ThisItemsPrices { get; set; }

        



        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}