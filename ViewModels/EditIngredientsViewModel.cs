using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;

namespace Tomasos4.ViewModels
{
    public class EditIngredientsViewModel
    {


        public int id { get; set; }
        public string Name { get; set; }
        public List<Produkt> Ingredients { get; set; }
        public Produkt CurrentProdukt { get; set; }


    }
}
