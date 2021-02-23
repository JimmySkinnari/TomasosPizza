using System.ComponentModel.DataAnnotations;
using Tomasos4.ModelsIdentity;

namespace Tomasos4.ViewModels
{
    public class UpdateUserViewModel
    {

        public ApplicationUser CurrentUser { get; set; }

        public string Id { get; set; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 256 characters.")]
        [RegularExpression("^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = ("Invalid format"))]
        public string UserName { get; set; }

        [Display(Name = "Name: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 100 characters.")]
        [RegularExpression("^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = ("Invalid format"))]
        public string Name { get; set; }

        [Display(Name = "Address: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string StreetAddress { get; set; }

        [Display(Name = "Zipcode: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string ZipCode { get; set; }

        [Display(Name = "Region: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string Region { get; set; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Email { get; set; }

        [Display(Name = "Phone: ")]
        [StringLength(50, ErrorMessage = "Max 50 tecken")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password needs to contain 1 capital char and one number")]
        [StringLength(20, ErrorMessage = "Max 20 tecken.")]
        public string Password { get; set; }


        [Display(Name = "Repeate Password: ")]
        [Required(ErrorMessage = "Password needs to contain 1 capital char and one number")]
        [StringLength(20, ErrorMessage = "Max 20 tecken.")]
        [Compare("Password", ErrorMessage ="Passwords don't match")]
        public string PasswordRepeate { get; set; }


        //public ApplicationUser GetApplicationUser()
        //{
        //    ApplicationUser user = new ApplicationUser();
        //    user.UserName = UserName;
        //    user.Namn = Name;
        //    user.Email = Email;
        //    user.Id = Id;
        //    user.Postnr = ZipCode;
        //    user.Postort = Region;
        //    user.Gatuadress = StreetAddress;
        //    user.PhoneNumber = PhoneNumber;


        //    return user;
        //}    
    }
}
