using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;

namespace Tomasos4.ViewModels
{
    public class EditDishViewModel
    {

        public int id { get; set; }
        public string Name { get; set; }

        public string Ingredients { get; set; }
        public Matratt CurrentMatratt { get; set; }
        public string CurrentMatrattDishTypeID { get; set; }
        public List<Produkt> Produkts { get; set; }
        public List<Matratt> Dishes { get; set; }

        public List<SelectListItem> DishTypes { get; set; }

        public List<IngredientViewModel> IngredientCheckBoxes { get; set; }
        public List<DishViewModel> AllDishes { get; set; }

        public EditDishViewModel()
        {
            AllDishes = new List<DishViewModel>();
            DishTypes = new List<SelectListItem>();
            IngredientCheckBoxes = new List<IngredientViewModel>();
        }

    }
}
