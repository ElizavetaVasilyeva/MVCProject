using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using Site.Models;
using Site.Providers;

namespace Site.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            try
            {
                return View(userService.GetAllUsers()
             .Select(user => new UserViewModel()
             {
                 Id = user.Id,
                 Login = user.Login,
                 Email = user.Email,
                 Role = userService.GetRoleById(user.RoleId).Name
             }));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400,"Bad request");
            }
          
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserEntity user;
            try
            {
                user = userService.GetUserById(id);
               
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400,"Bad request");
            }
            UserViewModel model = new UserViewModel()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Role = userService.GetRoleById(user.RoleId).Name
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            bool anyUser;
            try
            {
                anyUser = userService.GetAllUsers().Any(u => u.Login.Contains(user.Login));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
           
            if (ModelState.IsValid)
            {
                UserEntity editUser;
                try
                {
                     editUser= userService.GetUserById(user.Id);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(400, "Bad request");
                }
                if (editUser.Login != user.Login && anyUser)
                {
                    ModelState.AddModelError("Login", "User with this login already exists");
                    return View(user);
                }
                editUser.Login = user.Login;
                editUser.Email = user.Email;
                editUser.RoleId = userService.GetRoleByName(user.Role).Id;       
                userService.UpdateUser(editUser);
            }
            return RedirectToAction("Index","Admin");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                userService.DeleteUser(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
           
            return RedirectToAction("Index","Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserModel viewModel)
        {
            bool anyUser;
            try
            {
                anyUser = userService.GetAllUsers().Any(u => u.Login.Contains(viewModel.Login));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            if (anyUser)
            {
                ModelState.AddModelError("Login", "User with this login already exists");
                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(viewModel.Login, viewModel.Email, viewModel.Password,viewModel.Role);
            }
            return RedirectToAction("Index", "Admin");
        }

    }
}
