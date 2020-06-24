using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ShopApp.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public ViewResult LogReg()
        {
            return View("LogReg");
        }

        [HttpPost("users/register")]
        public IActionResult Register(LogRegWrapper FromForm)
        {
            if (ModelState.IsValid)
            {
                // Unique validation
                if (dbContext.Users.Any(u => u.Email == FromForm.Register.Email))
                {
                    ModelState.AddModelError("Register.Email", "Already registered? Please Log In.");
                    return LogReg();
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                FromForm.Register.Password = Hasher.HashPassword(FromForm.Register, FromForm.Register.Password);

                dbContext.Add(FromForm.Register);
                dbContext.SaveChanges();


                HttpContext.Session.SetInt32("UserId", FromForm.Register.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return LogReg();
            }
        }

        [HttpPost("users/login")]
        public IActionResult Login(LogRegWrapper FromForm)
        {
            if (ModelState.IsValid)
            {
                User InDb = dbContext.Users.FirstOrDefault(u => u.Email == FromForm.Login.Email);

                if (InDb == null)
                {
                    ModelState.AddModelError("Login.Email", "Invalid email/password");
                    return LogReg();
                }

                PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
                PasswordVerificationResult Result = Hasher.VerifyHashedPassword(FromForm.Login, InDb.Password, FromForm.Login.Password);

                if (Result == 0)
                {
                    ModelState.AddModelError("Login.Email", "Invalid email/password");
                    return LogReg();
                }
                HttpContext.Session.SetInt32("UserId", InDb.UserId);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return LogReg();
            }
        }

        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogReg");
        }


        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }

            DashboardWrapper DashWrap = new DashboardWrapper()
            {
                AllStores = dbContext.Stores
                .Include(i => i.Items)
                .ToList(),

                AllIngredients = dbContext.Ingredients
                .Include(i => i.Products)
                .ToList(),

                AllCatigories = dbContext.Categories
                .Include(p => p.Products)
                .ToList(),

                AllProducts = dbContext.Products
                .Include(u => u.User)
                .Include(i => i.Ingredients)
                .ToList(),

                LoggedUser = dbContext.Users
                    .FirstOrDefault(u => u.UserId == (int)LoggedId)
            };

            return View("Dashboard", DashWrap);
        }


        [HttpGet("add/store")]
        public IActionResult AddStore()

        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }

            return View("AddStore");
        }

        [HttpPost("process/store")]
        public IActionResult ProcessStore(Store FromForm)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            if (ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("AllStores");
            }
            else
            {
                return AddStore();
            }
        }

        [HttpGet("allstores")]
        public IActionResult AllStores()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if(LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }

            AllStoresWrapper ASWrap = new AllStoresWrapper()
            {
                AllStores = dbContext.Stores
                .ToList(),

            };

            return View("AllStores", ASWrap);
        }

        [HttpGet("stores/{StoreId}")]
        public IActionResult SingleStore(int StoreId)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if(LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("SingleStore");
        }



        [HttpGet("add/product")]
        public IActionResult AddProduct()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("AddProduct");
        }

        [HttpPost("process/product")]
        public IActionResult ProcessProduct(Store FromForm)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            if (ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("AllProducts");
            }
            else
            {
                return AddProduct();
            }
        }

        [HttpGet("allproducts")]
        public IActionResult AllProducts()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }

            AllProductsWrapper APWrap = new AllProductsWrapper()
            {
                AllProducts = dbContext.Products
                .ToList(),

            };

            return View("AllProducts", APWrap);
        }

        [HttpGet("products/{ProductId}")]
        public IActionResult SingleProduct(int ProductId)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("SingleProduct");
        }




        [HttpGet("add/ingredient")]
        public IActionResult AddIngredient()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("AddIngredient");
        }

        [HttpPost("process/ingredient")]
        public IActionResult ProcessIngredient(Ingredient FromForm)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            if (ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("AllIngredients");
            }
            else
            {
                return AddIngredient();
            }
        }

        [HttpGet("allingredients")]
        public IActionResult AllIngredients()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }

            AllIngredientsWrapper AIWrap = new AllIngredientsWrapper()
            {
                AllIngredients = dbContext.Ingredients
                .ToList(),

            };

            return View("AllIngredients", AIWrap);
        }

        [HttpGet("ingredients/{IngredientId}")]
        public IActionResult SingleIngredient(int IngredientId)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("SingleIngredient");
        }



        [HttpGet("add/category")]
        public IActionResult AddCategory()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("AddCategory");
        }

        [HttpPost("process/category")]
        public IActionResult ProcessCategory(Category FromForm)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            if (ModelState.IsValid)
            {
                dbContext.Add(FromForm);
                dbContext.SaveChanges();
                return RedirectToAction("AllCategories");
            }
            else
            {
                return AddCategory();
            }
        }

        [HttpGet("allcategories")]
        public IActionResult AllCategories()
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }

            AllCategoriesWrapper ACWrap = new AllCategoriesWrapper()
            {
                AllCategories = dbContext.Categories
                .ToList(),

            };

            return View("AllCategories", ACWrap);
        }

        [HttpGet("categories/{CategoryId}")]
        public IActionResult SingleCategory(int CategoryId)
        {
            int? LoggedId = HttpContext.Session.GetInt32("UserId");
            if (LoggedId == null)
            {
                return RedirectToAction("LogReg");
            }
            return View("SingleCategory");
        }




// Lookup into this, we'll prop need a change
        // [HttpGet("add/item")]
        // public IActionResult AddItem()
        // {
        //     int? LoggedId = HttpContext.Session.GetInt32("UserId");
        //     if (LoggedId == null)
        //     {
        //         return RedirectToAction("LogReg");
        //     }
        //     return View("AddItem");
        // }










        // path('check_in', views.check_in),??????????????????
        // path('add_product', views.product_form),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('delete_product/<int:product_id>', views.delete_product),?????????????????????
        // path('process_product', views.process_product_form),!!!!!!!!!!!!!!!!!!!!!
        // path('product/<int:product_id>', views.single_product),!!!!!!!!!!!!!!!!!!!!
        // path('products', views.all_products),!!!!!!!!!!!!!!!!!!!
        // path('add_item', views.item_form),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('process_item', views.process_item_form),
        // path('item/<int:item_id>', views.single_item),
        // path('items', views.all_items),
        // path('store/<int:store_id>', views.single_store),!!!!!!!!!!!!!!!!!
        // path('stores', views.stores),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('add_store', views.add_store_form),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('process_store', views.process_store_form),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('add_ingredient', views.add_ingredients_form),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('ingredient/<int:ingredient_id>', views.single_ingredient),!!!!!!!!!!!!!!!!!!
        // path('ingredients', views.ingredients),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('process_ingredients', views.process_ingredients_form),!!!!!!!!!!!!!!!!!!
        // path('add_category', views.add_categories_form),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('categories', views.categories),!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // path('process_categories', views.process_categories_form),!!!!!!!!!!!!!!!!!!!!!





































        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
