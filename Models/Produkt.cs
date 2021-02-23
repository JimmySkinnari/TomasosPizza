using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tomasos4.Models
{
    public partial class Produkt
    {
        public Produkt()
        {
            MatrattProdukts = new HashSet<MatrattProdukt>();
        }

        public int ProduktId { get; set; }
        [Required(ErrorMessage = "*")]
        public string ProduktNamn { get; set; }

        public virtual ICollection<MatrattProdukt> MatrattProdukts { get; set; }
    }
}
