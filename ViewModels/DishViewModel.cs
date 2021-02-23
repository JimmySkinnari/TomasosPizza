using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;

namespace Tomasos4.ViewModels
{
    public class DishViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string IngredientsString { get; set; }
        public string Description { get; set; }

        public string DishType { get; set; }


        public Matratt CurrentMatratt { get; set; }

        public List<SelectListItem> DishTypes { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }


        public DishViewModel(int id, string name, string ingredients, string description)
        {
            Id = id;
            Name = name;
            IngredientsString = ingredients;
            Description = description;
        }

        public DishViewModel()
        {

        }


    }
}
