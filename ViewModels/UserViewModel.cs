using System.Collections.Generic;
using Tomasos4.Models;
using Tomasos4.ViewModels;

namespace Tomasos4.ViewModels
{
    public class UserViewModel
    {

        public string SelectedUserID { get; set; }

        public List<UserRolesViewModel> Users { get; set; }

        public UserViewModel()
        {

            Users = new List<UserRolesViewModel>();

        }

        public UserViewModel(string selectedUserid)
        {

            Users = new List<UserRolesViewModel>();
            SelectedUserID = selectedUserid;


        }

    }
}
