using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;

namespace Tomasos4.ViewModels
{
    public class MenuViewModel
    {

        public List<DishViewModel> Menu { get; set; }
        public List<SelectListItem> DishTypes { get; set; }


    }
}
