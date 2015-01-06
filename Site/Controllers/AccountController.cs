using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using Site.Infrastructura;
using Site.Models;
using Site.Providers;

namespace Site.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.RememberMe);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong password or login");
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(viewModel);
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel viewModel)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Text from the image in an incorrect");
                return View(viewModel);
            }
            bool anyUser;
            try
            {
                anyUser = userService.GetAllUsers().Any(u => u.Login.Contains(viewModel.Login));
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Account");
            }

            if (anyUser)
            {
                ModelState.AddModelError("Login", "User with this login already exists");
                return View(viewModel);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(viewModel.Login, viewModel.Email, viewModel.Password, "User");

                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error registreting");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Account");
            }
            return View(viewModel);
        }
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            ci.Dispose();
            return null;
        }
    }
}
