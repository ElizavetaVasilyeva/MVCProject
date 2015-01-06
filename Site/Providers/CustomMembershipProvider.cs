using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using Ninject;
using System.Web.Mvc;
using DependencyResolver;

namespace Site.Providers
{
    public class CustomMembershipProvider:MembershipProvider
    {
        public IUserService userService
        {
            get { return System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>(); }
        }


        public MembershipUser CreateUser(string login,string email, string password,string roleUser)
        {
            MembershipUser membershipUser = GetUser(login, false);

            if (membershipUser != null)
            {
                return null;
            }

            var user = new UserEntity
            {
                Login=login,
                Email = email,
                Password = Crypto.HashPassword(password),
                CreatedDate = DateTime.Now
            };
            var role = userService.GetRoleByName(roleUser);
            if (HttpContext.Current.User.IsInRole("Admin"))
            {
                role = userService.GetRoleByName(roleUser);
            }
            if (role != null)
            {
                user.RoleId = role.Id;
            }
            userService.CreateUser(user);
            membershipUser = GetUser(login, false);
            return membershipUser;
        }

        public override bool ValidateUser(string login, string password)
        {
            var user = userService.GetUserByLogin(login);
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            var existUser = userService.UserExistsByLogin(login);
            if (!existUser) return null;
            var user = userService.GetUserByLogin(login);
            

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, null, null, null,
                false, false, user.CreatedDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }


        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

       
    }
}