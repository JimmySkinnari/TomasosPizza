using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tomasos4.Models
{
    public partial class BestallningMatratt
    {
        [Required(ErrorMessage = "*")]
        public int MatrattId { get; set; }
        [Required(ErrorMessage = "*")]
        public int BestallningId { get; set; }
        [Required(ErrorMessage = "*")]
        public int Antal { get; set; }

        public virtual Bestallning Bestallning { get; set; }
        public virtual Matratt Matratt { get; set; }
    }
}
