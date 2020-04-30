using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PhoneBill.Models
{
    class MyCustomRoleProvider : RoleProvider
    {
        private MyDbContext db = new MyDbContext();
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //throw new NotImplementedException();



            //-----We trim UNDPAF\\ than converting it to character array----//
            var TrimedUser = username.TrimStart("UNOCHA\\".ToCharArray());
            //var ReplaceTheDotWithSpace = TrimedUser.Replace(@".", " ");
            var TheUser = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (TheUser != null)
            {
                var TheRoles =
                     from r in db.MyRolesContext
                     where TheUser.RoleId == r.Id
                     select r.RoleName;
                if (TheRoles != null)
                {
                    return TheRoles.ToArray();
                }
                else
                {
                    return new string[] { };
                }
            }
            else
            {
                return new string[] { "Vieweer" };
            }
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            //throw new NotImplementedException();
            //-----We trim UNDPAF\\ than converting it to character array----//
            var TrimedUser = username.TrimStart("UNDPAF\\".ToCharArray());
            //var ReplaceTheDotWithSpace = TrimedUser.Replace(@".", " ");
            var TheUser = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));

            var TheRoles = from r in db.MyRolesContext
                           where TheUser.RoleId == r.Id
                           select r.RoleName;
            if (TheUser != null)
            {
                return TheRoles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            this.Dispose();
          
        }
    }
}