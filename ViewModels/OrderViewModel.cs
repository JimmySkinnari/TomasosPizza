using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos4.Models;

namespace Tomasos4.ViewModels
{
    public class OrderViewModel
    {

        public List<Bestallning> Orders { get; set; }

        public Bestallning CurrentOrder { get; set; }

        public int ID { get; set; }

    }
}
