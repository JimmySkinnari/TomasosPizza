using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Tomasos4.Models;

namespace Tomasos4.ViewModels
{
    public class AddDishViewModel
    {
        public Matratt CurrentMatratt { get; set; }

        public string CurrentMatrattDishTypeID { get; set; }
        public List<Produkt> Produkts { get; set; }

        public List<SelectListItem> DishTypes { get; set; }

        public List<IngredientViewModel> IngredientCheckBoxes { get; set; }

        public AddDishViewModel()
        {
            CurrentMatratt = new Matratt();
        }


    }
}
