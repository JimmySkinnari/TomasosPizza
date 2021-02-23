using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;
using Tomasos4.ModelsIdentity;
using Tomasos4.ViewModels;

namespace Tomasos4.Controllers
{
    public class FoodController : Controller
    {

        private TomasosContext _context;
        private UserManager<ApplicationUser> _userManager;

        public FoodController(TomasosContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            MenuViewModel model = new MenuViewModel();
            model.Menu = GetMenu();

            return View("Cart", model);
        }

        private List<DishViewModel> GetMenu()
        {

            var model = new List<DishViewModel>();
            var dishes = _context.Matratts.Include(r => r.MatrattProdukts).Include(t => t.MatrattTyp).ToList();

            foreach (var item in dishes)
            {
                DishViewModel dish = new DishViewModel();

                dish.Id = item.MatrattId;
                dish.Name = item.MatrattNamn;
                dish.IngredientsString = GetIngredients(item);
                dish.DishType = item.MatrattTyp.Beskrivning;
                dish.Description = item.Beskrivning;
                
                model.Add(dish);
            }

            return model;
        }

        private string GetIngredients(Matratt dish)
        {
            string result = "";

            foreach (var item in dish.MatrattProdukts)
            {
                var produkt = _context.Produkts.SingleOrDefault(x => x.ProduktId == item.ProduktId);
                result += produkt.ProduktNamn + ", ";
            }

            return result;

        }
    }
}
