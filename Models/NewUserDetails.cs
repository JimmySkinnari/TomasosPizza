using System.ComponentModel.DataAnnotations;

namespace Tomasos4.Models
{
    public class NewUserDetails
    {
        [StringLength(100, ErrorMessage = "Max length is 100 characters")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(256, ErrorMessage = "Max length is 256 characters")]
        [Display(Name = "Username: ")]
        public string Username { get; set; }


        
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]

        public string Password { get; set; }

        [Display(Name = "Repeate Password: ")]
        [Required(ErrorMessage = "*")]
        public string PasswordRepeate { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(256, ErrorMessage = "Max length is 256 characters")]
        [EmailAddress(ErrorMessage = "Incorrect Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max length is 50 characters")]
        public string StreetAddress { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max length is 20 characters")]
        public string Zipcode { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max length is 100 characters")]
        public string Region { get; set; }

        public string RoleName { get; set; }


        public string PhoneNumber { get; set; }



        public NewUserDetails()
        {
            RoleName = "RegularUser";
        }


    }
}
