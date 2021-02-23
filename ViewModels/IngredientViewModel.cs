using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tomasos4.ViewModels
{
    public class IngredientViewModel
    {

        public int Id { get; set; }
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public bool IsSelected { get; set; }


        public IngredientViewModel(int ingredientID, string ingredientName)
        {
            IngredientID = ingredientID;
            IngredientName = ingredientName;

        }

        public IngredientViewModel()
        {

        }

    }
}
