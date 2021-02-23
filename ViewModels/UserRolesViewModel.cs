using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tomasos4.ViewModels
{
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }

        public string Username { get; set; }

        public string RoleName { get; set; }

        public bool IsSelected { get; set; }

    }
}
