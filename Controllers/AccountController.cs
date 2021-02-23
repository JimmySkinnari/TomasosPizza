using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;
using Tomasos4.ModelsIdentity;
using Tomasos4.ViewModels;

namespace Tomasos4.Controllers
{
    public class AccountController : Controller
    {



        private SignInManager<ApplicationUser> _signinmanager;
        private UserManager<ApplicationUser> _usermanager;
        private TomasosContext _context;

        // injects 
        public AccountController(SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager,
                                  TomasosContext context)
        {
            _signinmanager = signInManager;
            _usermanager = userManager;
            _context = context;
        }


        // Actions regarding account and identity
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(NewUserDetails user)
        {
            //Kontrollerar inloggningen mot databasen
            var result = await _signinmanager.PasswordSignInAsync(user.Name, user.Password, false, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Felaktig inloggning";
                return View();
            }

        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            //Loggar man ut användaren försvinner authentication cookien
            await _signinmanager.SignOutAsync();
            ClearCartData();


            return RedirectToAction("Index", "Home");
        }

        private void ClearCartData()
        {

            if (HttpContext.Session.GetString("cart") != null)
            {
                HttpContext.Session.Clear();
            }

        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(NewUserDetails newUser)
        {


            if (!IsPasswordMatching(newUser))
            {
                ViewBag.Message = "Password does not match";
                return View();
            }

            ApplicationUser newAccount = new ApplicationUser()
            {
                UserName = newUser.Username,
                Email = newUser.Email,
                Namn = newUser.Name,
                PhoneNumber = newUser.PhoneNumber,
                Postnr = newUser.Zipcode,
                Gatuadress = newUser.StreetAddress,
                Postort = newUser.Region
            };



            // Creates and adds new user to the db. Pw Will be controlled to meet all requirements.
            var result = await _usermanager.CreateAsync(newAccount, newUser.Password);

            //Koppla användaren till en roll
            await _usermanager.AddToRoleAsync(newAccount, newUser.RoleName);



            if (result.Succeeded)
            {

                await _signinmanager.SignInAsync(newAccount, isPersistent: true);
                return RedirectToAction("Index", "Home");

            }
            else
            {

                ViewBag.Message = "Information given is not correct. Please try again";
                return View();

            }



        }

        private bool IsPasswordMatching(NewUserDetails newUser)
        {
            if (newUser.Password == newUser.PasswordRepeate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<IActionResult> EditUser()
        {
            var currentUser = await _usermanager.FindByNameAsync(User.Identity.Name);


            var model = new UpdateUserViewModel();

            model.CurrentUser = currentUser;

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserViewModel model)
        {


            var user = await _usermanager.FindByIdAsync(model.Id);
            user.Email = model.Email;
            user.Gatuadress = model.StreetAddress;
            user.Postnr = model.ZipCode;
            user.PhoneNumber = model.PhoneNumber;
            user.Namn = model.Name;
            user.Postort = model.Region;

            var result = await _usermanager.UpdateAsync(user);

            if (result.Succeeded)
            {

                model.CurrentUser = user;
                ViewBag.Message = "User Updated succesfully.";

            }
            else
            {
                ViewBag.Message = "Something went wrong.";
            }



            return View(model);


        }

        public IActionResult BuyProducts()
        {
            var model = GetCartItems();

            return View(model);
        }

        public async Task<IActionResult> BuyConfirmed()
        {

            ApplicationUser user = await _usermanager.GetUserAsync(User);
            var cartItems = GetCartItems();
            int totalPrice = cartItems.Sum(x => x.Pris);


            if (User.IsInRole("PremiumUser") && cartItems.Count >= 3)
            {
                totalPrice = Convert.ToInt32(totalPrice * 0.8);

                user.Points += (cartItems.Count * 10);

                if (user.Points >= 100)
                {
                    totalPrice = totalPrice - cartItems.OrderBy(m => m.Pris).First().Pris;
                    user.Points = 0;
                }

                await _usermanager.UpdateAsync(user);
            }


            var order = new Bestallning()
            {
                Id = user.Id,
                BestallningDatum = DateTime.Now,
                Levererad = false,
                Totalbelopp = totalPrice
              
            };

            _context.Bestallnings.Add(order);
            _context.SaveChanges();

            var sortedList = cartItems.GroupBy(m => m.MatrattId).Select(m => m.First()).ToList();

            foreach (var item in sortedList)
            {
               
                var ratt = new BestallningMatratt()
                {
                    MatrattId = item.MatrattId,
                    BestallningId = order.BestallningId,
                    Antal = cartItems.Where(m => m.MatrattId == item.MatrattId).ToList().Count
                };

                _context.BestallningMatratts.Add(ratt);
                _context.SaveChanges();


            }




            _context.SaveChanges();

            HttpContext.Session.SetString("cart", "");

            return View();

        }

        private List<Matratt> GetCartItems()
        {
            List<Matratt> cart = new List<Matratt>();

            var jsonCart = HttpContext.Session.GetString("cart");


            cart = JsonConvert.DeserializeObject<List<Matratt>>(jsonCart);

            return cart;

        }
    }
}

