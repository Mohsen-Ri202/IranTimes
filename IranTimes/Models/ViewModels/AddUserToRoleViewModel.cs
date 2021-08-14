using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes
{
    public class AddUserToRoleViewModel
    {
        public AddUserToRoleViewModel()
        {
            UserRoleNames = new List<UserRoleName>();

        }
       
        public string Id { get; set; }
        public List<UserRoleName> UserRoleNames { get; set; }

    }

    public class UserRoleName
    {
        //public UserRoleName(string rolename)
        //{
        //    RoleName = rolename;
        //}
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
