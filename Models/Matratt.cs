using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tomasos4.Models
{
    public partial class Matratt
    {
        public Matratt()
        {
            BestallningMatratts = new HashSet<BestallningMatratt>();
            MatrattProdukts = new HashSet<MatrattProdukt>();
        }

        public int MatrattId { get; set; }
        [Required(ErrorMessage ="*")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string MatrattNamn { get; set; }

        [StringLength(200, ErrorMessage = "Max 200 characters")]
        public string Beskrivning { get; set; }

        [Required(ErrorMessage = "*")]
        public int Pris { get; set; }
        [Required(ErrorMessage = "*")]
        public int MatrattTypId { get; set; }

        public virtual MatrattTyp MatrattTyp { get; set; }
        public virtual ICollection<BestallningMatratt> BestallningMatratts { get; set; }
        public virtual ICollection<MatrattProdukt> MatrattProdukts { get; set; }
    }
}
