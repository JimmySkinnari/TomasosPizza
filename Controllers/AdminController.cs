using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;
using Tomasos4.ModelsIdentity;
using Tomasos4.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Tomasos4.Controllers
{

    //Detta gör att bara användare som är inloggade som admin kommer åt detta
    [Authorize(Roles = "Administrator")]

    public class AdminController : Controller
    {

        private TomasosContext _context;
        private UserManager<ApplicationUser> _userManager;



        public AdminController(TomasosContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> EditUsers()
        {
            UserViewModel model = new UserViewModel();
            model.Users = await GetUsersAsync();


            return View(model);
        }

        public IActionResult EditFoodMenu()
        {

            EditDishViewModel model = new EditDishViewModel();

            var dishes = _context.Matratts.Include(r => r.MatrattProdukts).Include(t => t.MatrattTyp).ToList();

            foreach (var item in dishes)
            {
                DishViewModel dish = new DishViewModel();

                dish.Id = item.MatrattId;
                dish.Name = item.MatrattNamn;
                dish.IngredientsString = GetIngredients(item);
                dish.DishType = item.MatrattTyp.Beskrivning;
                dish.Description = item.Beskrivning;
                dish.Ingredients = GetCheckBoxDataForDish(item.MatrattId);

                model.AllDishes.Add(dish);

            }


            return View(model);
        }

        private List<IngredientViewModel> GetCheckBoxDataForDish(int id)
        {
            var dish = _context.Matratts.SingleOrDefault(x => x.MatrattId == id);
            var model = new List<IngredientViewModel>();

            var prod = _context.Produkts.ToList();

            foreach (var item in prod)
            {
                var ingredientViewModel = new IngredientViewModel(item.ProduktId, item.ProduktNamn);

                if (IsDishContainsProduct(item, dish.MatrattId))
                {
                    ingredientViewModel.IsSelected = true;
                }
                else
                {
                    ingredientViewModel.IsSelected = false;
                }

                model.Add(ingredientViewModel);

            }

            return model;

        }

        private List<IngredientViewModel> GetCheckBoxDataForDishes()
        {
            var model = new List<IngredientViewModel>();

            var prod = _context.Produkts.ToList();

            foreach (var item in prod)
            {
                var ingredientViewModel = new IngredientViewModel(
                                                                   item.ProduktId,
                                                                   item.ProduktNamn);
                ingredientViewModel.IsSelected = false;
                model.Add(ingredientViewModel);
            }

            return model;

        }

        private List<IngredientViewModel> GetCheckBoxData()
        {
            var model = new List<IngredientViewModel>();
            var prod = _context.Produkts.ToList();

            foreach (var item in prod)
            {
                var ingredientViewModel = new IngredientViewModel(item.ProduktId, item.ProduktNamn);
                ingredientViewModel.IsSelected = false;


                model.Add(ingredientViewModel);

            }

            return model;

        }

        private bool IsDishContainsProduct(Produkt ingredient, int id)
        {

            var matType = _context.MatrattProdukts.ToList();

            foreach (var item in matType)
            {
                if (item.MatrattId == id && ingredient.ProduktId == item.ProduktId)
                {
                    return true;
                }
            }
            return false;
        }



        private List<SelectListItem> GetDishTypes()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var types = _context.MatrattTyps.ToList();

            foreach (var item in types)
            {
                list.Add(new SelectListItem(item.Beskrivning, item.MatrattTypId.ToString()));

            }

            return list;
        }

        public IActionResult EditDish(int id)
        {

            EditDishViewModel model = new EditDishViewModel();
            model.DishTypes = GetDishTypes();
            model.IngredientCheckBoxes = GetCheckBoxDataForDish(id);
            model.CurrentMatratt = _context.Matratts.SingleOrDefault(m => m.MatrattId == id);

            return View("EditFoodMenu", model);

        }

        [HttpPost]
        public IActionResult EditDish(EditDishViewModel model)
        {
            var editedDish = _context.Matratts.SingleOrDefault(x => x.MatrattId == model.CurrentMatratt.MatrattId);
            editedDish.MatrattNamn = model.CurrentMatratt.MatrattNamn;
            editedDish.Beskrivning = model.CurrentMatratt.Beskrivning;
            editedDish.Pris = model.CurrentMatratt.Pris;
            if (model.CurrentMatrattDishTypeID != null)
            {
                editedDish.MatrattTypId = int.Parse(model.CurrentMatrattDishTypeID);
            }

            _context.SaveChanges();

            UpdateIngredients(editedDish, model.IngredientCheckBoxes);




            ViewBag.Message = "Changes saved";

            return RedirectToAction("EditFoodMenu", "Admin");

        }

        private void UpdateIngredients(Matratt editedDish, List<IngredientViewModel> ingredientCheckBoxes)
        {

            RemoveAllIngredientsFromDish(editedDish);
            AddIngredientsToDish(editedDish, ingredientCheckBoxes);

        }

        private void AddIngredientsToDish(Matratt dish, List<IngredientViewModel> ingredientCheckBoxes)
        {
            foreach (var item in ingredientCheckBoxes)
            {
                if (item.IsSelected == true)
                {
                    MatrattProdukt mr = new MatrattProdukt();
                    mr.MatrattId = dish.MatrattId;
                    mr.ProduktId = item.IngredientID;

                    _context.MatrattProdukts.Add(mr);
                    _context.SaveChanges();
                }

            }
        }



        public IActionResult AddDish()
        {

            AddDishViewModel model = new AddDishViewModel();

            //var dishes = _context.Matratts.Include(r => r.MatrattProdukts).Include(t => t.MatrattTyp).ToList();

            //foreach (var item in dishes)
            //{
            //    DishViewModel dish = new DishViewModel();

            //    dish.Id = item.MatrattId;
            //    dish.Name = item.MatrattNamn;
            //    dish.IngredientsString = GetIngredients(item);
            //    dish.DishType = item.MatrattTyp.Beskrivning;
            //    dish.Description = item.Beskrivning;
            //    dish.Ingredients = GetCheckBoxDataForDish(item.MatrattId);


            //    model.AllDishes.Add(dish);

            //}


            model.DishTypes = GetDishTypes();
            model.IngredientCheckBoxes = GetCheckBoxDataForDishes();


            return View(model);
        }


        [HttpPost]
        public IActionResult AddDish(AddDishViewModel model)
        {

            var dish = new Matratt();

            if (!ModelState.IsValid)
            {
                model.DishTypes = GetDishTypes();
                model.IngredientCheckBoxes = GetCheckBoxDataForDishes();
                return View(model);
            }

            try
            {

                dish.Beskrivning = model.CurrentMatratt.Beskrivning;
                dish.Pris = model.CurrentMatratt.Pris;
                dish.MatrattNamn = model.CurrentMatratt.MatrattNamn;
                dish.MatrattTypId = int.Parse(model.CurrentMatrattDishTypeID);


                _context.Matratts.Add(dish);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                ViewBag.Message = "Please correct inputs";
                return View(model);

            }





            foreach (var item in model.IngredientCheckBoxes)
            {
                if (item.IsSelected == true)
                {

                    MatrattProdukt newProd = new MatrattProdukt();

                    newProd.MatrattId = dish.MatrattId;
                    newProd.ProduktId = item.IngredientID;

                    _context.MatrattProdukts.Add(newProd);
                    _context.SaveChanges();

                }
            }




            ViewBag.Message = "Dish saved to the menu!";
            return RedirectToAction("EditFoodMenu", "Admin");


        }

        private void RemoveAllIngredientsFromDish(Matratt dish)
        {
            var toDelete = _context.MatrattProdukts.Where(x => x.MatrattId == dish.MatrattId).ToList();

            foreach (var item in toDelete)
            {
                _context.MatrattProdukts.Remove(item);
                _context.SaveChanges();
            }

        }

        public IActionResult DeleteDish(int id)
        {
            if (ModelState.IsValid)
            {
                var dishToDelete = _context.Matratts.SingleOrDefault(p => p.MatrattId == id);

                RemoveAllIngredientsFromDish(dishToDelete);

                _context.Matratts.Remove(dishToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("EditFoodMenu", "Admin");
        }


        public IActionResult EditIngredients()
        {
            EditIngredientsViewModel model = new EditIngredientsViewModel();

            model.Ingredients = _context.Produkts.ToList();
            return View(model);

        }


        public IActionResult EditProdukt(int id)
        {
            EditIngredientsViewModel model = new EditIngredientsViewModel();

            model.CurrentProdukt = _context.Produkts.SingleOrDefault(p => p.ProduktId == id);

            return View(model);

        }

        [HttpPost]
        public IActionResult EditProdukt(EditIngredientsViewModel model)
        {

            var prodToEdit = _context.Produkts.SingleOrDefault(x => x.ProduktId == model.CurrentProdukt.ProduktId);

            prodToEdit.ProduktNamn = model.CurrentProdukt.ProduktNamn;

            _context.SaveChanges();
            ViewBag.Message = "Changes saved";



            return RedirectToAction("EditIngredients", "Admin");

        }

        [HttpGet]
        public IActionResult DeleteProdukt(int id)
        {

            var toDelete = _context.MatrattProdukts.Where(x => x.ProduktId == id).ToList();

            foreach (var item in toDelete)
            {
                _context.MatrattProdukts.Remove(item);
                _context.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                var prodToDelete = _context.Produkts.SingleOrDefault(p => p.ProduktId == id);

                _context.Produkts.Remove(prodToDelete);

                _context.SaveChanges();

            }

            return RedirectToAction("EditIngredients", "Admin");

        }

        [HttpPost]
        public IActionResult AddProdukt(EditIngredientsViewModel model)
        {

            if (ModelState.IsValid)
            {

                _context.Produkts.Add(model.CurrentProdukt);

                _context.SaveChanges();

            }

            return RedirectToAction("EditIngredients", "Admin");

        }



        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("EditUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("EditUsers");
            }
        }





        public async Task<List<UserRolesViewModel>> GetUsersAsync()
        {
            List<UserRolesViewModel> model = new List<UserRolesViewModel>();
            var users = _userManager.Users.ToList();



            foreach (var user in users)
            {
                UserRolesViewModel userData = new UserRolesViewModel();
                var result = await _userManager.FindByIdAsync(user.Id);

                if (!await _userManager.IsInRoleAsync(result, "Administrator"))
                {
                    userData.UserId = result.Id;
                    userData.Username = result.UserName;

                    var rolename = await _userManager.GetRolesAsync(user);
                    userData.RoleName = rolename[0].ToString();
                    model.Add(userData);
                }
            }

            return model;
        }

        public IActionResult EditOrders()
        {
            var model = new OrderViewModel();
            var orders = _context.Bestallnings.ToList();
            model.Orders = orders;


            return View(model);
        }


        [HttpGet]
        public IActionResult DeleteOrder(OrderViewModel model)
        {
            var orderDataToDelete = _context.BestallningMatratts.Where(x => x.BestallningId == model.ID).ToList();

            foreach (var item in orderDataToDelete)
            {
                _context.BestallningMatratts.Remove(item);
                _context.SaveChanges();
            }

            var orderToDelete = _context.Bestallnings.SingleOrDefault(x => x.BestallningId == model.ID);
            _context.Bestallnings.Remove(orderToDelete);
            _context.SaveChanges();
            return RedirectToAction("EditOrders", "Admin");

        }

        [HttpGet]
        public IActionResult MarkOrderAsDelivered(OrderViewModel model)
        {
            var orderToEdit = _context.Bestallnings.SingleOrDefault(x => x.BestallningId == model.ID);

            orderToEdit.Levererad = true;
            _context.SaveChanges();


            return RedirectToAction("EditOrders", "Admin");


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



        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string id)
        {

            var model = await _userManager.FindByIdAsync(id);

            if (!await _userManager.IsInRoleAsync(model, "Admin"))
            {

                if (await _userManager.IsInRoleAsync(model, "PremiumUser"))
                {
                    await RemoveAllRoles(model);
                    await _userManager.AddToRoleAsync(model, "RegularUser");
                }
                else
                {
                    await RemoveAllRoles(model);
                    await _userManager.AddToRoleAsync(model, "PremiumUser");
                }
            }


            if (model == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
            }

            return RedirectToAction("EditUsers", "Admin");
        }

        private async Task RemoveAllRoles(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
        }
    }
}
