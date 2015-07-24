using System;
using System.Web.Security;
using System.Linq;

namespace TodoApp.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string UserId, string roleName)
        {
            using (var db = new AppContext())
            {
                var user = db.Users
                    .Where(u => u.Id == int.Parse(UserId))
                    .FirstOrDefault();

                string[] roles = user.Roles.Select(r => r.RoleName).ToArray();

                if (roles.Contains(roleName))
                {
                    return true;
                }
            }
            return false;
        }

        public override string[] GetRolesForUser(string UserId)
        {
            using (var db = new AppContext())
            {
                int id = int.Parse(UserId);
                var user = db.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefault();

                string[] roles = user.Roles.Select(r => r.RoleName).ToArray();

                return roles;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

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

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}