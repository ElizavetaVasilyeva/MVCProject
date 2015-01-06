using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interfaces;
using BLL.Interfaces.Services;
using Ninject;
using DependencyResolver;
using System.Web.Mvc;

namespace Site.Providers
{
    public class CustomRoleProvider:RoleProvider
    {
        public IUserService userService
        {
            get { return System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>(); }
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            var user = userService.GetUserByLogin(login);
            if (user == null) return false;
            var userRole = userService.GetRoleById(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;

        }

        public override string[] GetRolesForUser(string login)
        {
            var roles = new string[] { };
            var user = userService.GetUserByLogin(login);

            if (user == null) return roles;
            var userRole = userService.GetRoleById(user.RoleId);

            if (userRole != null)
            {
                roles = new string[] { userRole.Name};
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new RoleEntity() {Name = roleName};
            userService.CreateRole(newRole);
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