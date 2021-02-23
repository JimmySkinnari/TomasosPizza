using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Tomasos4.Models;
using Tomasos4.ViewModels;

namespace Tomasos4.Controllers
{
    public class CartController : Controller
    {
        private TomasosContext _context;

        public CartController(TomasosContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            MenuViewModel model = new MenuViewModel();
            model.Menu = GetMenu();

            return View(model);
        }


        public IActionResult AddDish(int id)
        {
            List<Matratt> cart;
            string jsonCart;

            //Kontroll om det är en tom varukorg
            if (HttpContext.Session.GetString("cart") == null)
            {
                //Ny tom varukorg
                cart = new List<Matratt>();

            }
            else
            {
                //Hämta det som finns i varukorgen
                jsonCart = HttpContext.Session.GetString("cart");

                //Konvertera dvs gör om till en lista med produkter jmför JSON.parse
                cart = JsonConvert.DeserializeObject<List<Matratt>>(jsonCart);

            }

            //Lägger in den nya produkten
            var dish = _context.Matratts.SingleOrDefault(p => p.MatrattId == id);
            cart.Add(dish);

            //Gör om vår produktlista till JSON jmför med JSON.stringify
            jsonCart = JsonConvert.SerializeObject(cart);

            //Lägg tillbaka den uppdaterade listan i sessionsvariabeln
            HttpContext.Session.SetString("cart", jsonCart);

            //Skickar vidare till översikten som skall uppdateras
            return PartialView("_CartOverviewPartial", cart);
        }

        public IActionResult RemoveDish()
        {
            return View();
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
