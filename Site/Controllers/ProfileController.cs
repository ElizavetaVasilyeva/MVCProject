using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Exseptions;
using BLL.Interfaces.Services;
using Site.Models;

namespace Site.Controllers
{
    public class ProfileController : Controller
    {
        private IUserService userService;

        public ProfileController(IUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Index(string name)
        {
            UserEntity user;
            ProfileEntity profile;
            try
            {
                user = userService.GetUserByLogin(name);
                profile = userService.GetProfileUser(user.Id);     
            }
            catch (ProfileNotFoundException)
            {
                if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    user = userService.GetUserByLogin(name);
                    return RedirectToAction("Index", "Gallery",new{name=user.Login});
                }
                return RedirectToAction("Create", "Profile");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(400, "Bad request");
            }
            ProfileViewModel viewProfile = new ProfileViewModel()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Email = profile.Email,
                Country = profile.Country,
                City = profile.City,
                PhoneNumber = profile.PhoneNumber,
                AboutYourself = profile.AboutYourself,
                Interests = profile.Interests,
                LastUpdateDate = profile.LastUpdateDate
            };
            ViewBag.Login = userService.GetUserById(profile.UserId).Login;
            return View(viewProfile);
            //}
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Countries = CountryModel.GetCountries();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProfileViewModel profile)
        {
            int idUser;
            try
            {
                idUser = userService.GetUserByLogin(System.Web.HttpContext.Current.User.Identity.Name).Id;
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "Profile");
            }

            if (ModelState.IsValid)
            {
                ProfileEntity newProfily = new ProfileEntity()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Age = profile.Age,
                    Email = profile.Email,
                    Country = profile.Country,
                    City = profile.City,
                    PhoneNumber = profile.PhoneNumber,
                    AboutYourself = profile.AboutYourself,
                    Interests = profile.Interests,
                    LastUpdateDate = DateTime.Now,
                    UserId = idUser
                };
                try
                {
                    userService.CreateProfile(newProfily);
                }
                catch (Exception)
                {
                    return RedirectToAction("Create", "Profile");
                }
            }
            return RedirectToAction("Index", "Profile", new { name = System.Web.HttpContext.Current.User.Identity.Name });
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int idUser;
            ProfileEntity profile;
            try
            {
                idUser = userService.GetUserByLogin(System.Web.HttpContext.Current.User.Identity.Name).Id;
                profile = userService.GetProfileUser(idUser);
            }
            catch (ProfileNotFoundException)
            {
                return RedirectToAction("Create", "Profile");
            }
            ProfileViewModel newProfile = new ProfileViewModel()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Email = profile.Email,
                Country = profile.Country,
                City = profile.City,
                PhoneNumber = profile.PhoneNumber,
                AboutYourself = profile.AboutYourself,
                Interests = profile.Interests,
                LastUpdateDate = profile.LastUpdateDate,
            };
            ViewBag.Countries = CountryModel.GetCountries();
            return View(newProfile);
        }

        [HttpPost]
        public ActionResult Edit(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                ProfileEntity newProfily = new ProfileEntity()
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Age = profile.Age,
                    Email = profile.Email,
                    Country = profile.Country,
                    City = profile.City,
                    PhoneNumber = profile.PhoneNumber,
                    AboutYourself = profile.AboutYourself,
                    Interests = profile.Interests,
                    LastUpdateDate = DateTime.Now,
                    UserId = userService.GetUserByLogin(System.Web.HttpContext.Current.User.Identity.Name).Id
                };
                try
                {
                    userService.UpdateProfile(newProfily);
                }
                catch (Exception)
                {
                    return RedirectToAction("Edit", "Profile");
                }

                return RedirectToAction("Index", "Profile", new { name = System.Web.HttpContext.Current.User.Identity.Name });
            }
            return RedirectToAction("Index", "Profile", new { name = System.Web.HttpContext.Current.User.Identity.Name });
        }
    }
}
