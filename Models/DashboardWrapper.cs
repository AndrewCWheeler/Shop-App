using System.Collections.Generic;

namespace ShopApp.Models
{
    public class DashboardWrapper
    {
        public User LoggedUser { get; set; }

        public List<Store> AllStores { get; set; }

        public List<Ingredient> AllIngredients { get; set; }


        public List<Product> AllProducts { get; set; }


        public List<Category> AllCatigories { get; set; }

    }
}