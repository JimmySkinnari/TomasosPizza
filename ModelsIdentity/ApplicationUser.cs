using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tomasos4.ModelsIdentity
{

    public class ApplicationUser : IdentityUser
    {


        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string Namn { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string Gatuadress { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        public string Postnr { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string Postort { get; set; }

        [PersonalData]
        [Column(TypeName = "int")]
        public int Points { get; set; }


    }
}
