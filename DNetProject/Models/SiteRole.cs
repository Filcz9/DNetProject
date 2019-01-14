using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DNetProject.Models
{
    public class SiteRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.username == username).FirstOrDefault();
                var roleid = user.role_id;
                var roleName = context.Roles.Where(a => a.id == roleid).FirstOrDefault().name;
                string[] roles = new string[1];
                roles[0] = roleName;
                return roles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                var roleId = context.Roles.Where(a => a.name == roleName).FirstOrDefault();
                var user = context.Users.Where(a => a.username == username).FirstOrDefault();
                if (user.role_id == roleId.id)
                {
                    return true;
                }
            }

            return false;
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