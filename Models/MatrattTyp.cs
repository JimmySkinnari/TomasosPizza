using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tomasos4.Models
{
    public partial class MatrattTyp
    {
        public MatrattTyp()
        {
            Matratts = new HashSet<Matratt>();
        }

        public int MatrattTypId { get; set; }

        [Required(ErrorMessage = "*")]
        public string Beskrivning { get; set; }

        public virtual ICollection<Matratt> Matratts { get; set; }
    }
}
