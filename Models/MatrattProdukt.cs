using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tomasos4.Models
{
    public partial class MatrattProdukt
    {
        [Required(ErrorMessage = "*")]
        public int MatrattId { get; set; }
        [Required(ErrorMessage = "*")]
        public int ProduktId { get; set; }

        public virtual Matratt Matratt { get; set; }
        public virtual Produkt Produkt { get; set; }
    }
}
