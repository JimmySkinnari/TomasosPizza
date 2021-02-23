using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Tomasos4.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Bestallnings = new HashSet<Bestallning>();
        }

        public string Id { get; set; }

        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 100 characters.")]
        [RegularExpression("^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = ("Invalid format"))]
        public string Namn { get; set; }

        [Display(Name = "Address: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Gatuadress { get; set; }

        [Display(Name = "Zipcode: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]

        public string Postnr { get; set; }



        [Display(Name = "Region: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string Postort { get; set; }
        public int? Points { get; set; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 256 characters.")]
        [RegularExpression("^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = ("Invalid format"))]
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }


        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }


        [Display(Name = "Phone: ")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Bestallning> Bestallnings { get; set; }
    }
}
