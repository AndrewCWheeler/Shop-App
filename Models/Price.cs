using System.ComponentModel.DataAnnotations;
using System;





namespace ShopApp.Models
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }

        public decimal Cost { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }



        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}