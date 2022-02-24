using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTL.Lib;
using PTL.Models;

namespace Web_GiaSu.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private AccountLib _service = new AccountLib();
        #region Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
         {
            // Nếu đăng nhập đúng => return url
            if (_service.Validate(model))
            {
                this.SetAuthenticationCookie(model);
                return RedirectToLocal(returnUrl);
            }
            // Nếu đăng nhập sai => return View
            else
            {
                ModelState.AddModelError("error", "Sai tên đăng nhập hoặc mật khẩu");
                return View(model);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public void SetAuthenticationCookie(LoginModel model)
        {
            HttpCookie cookie = new HttpCookie(CommonModel.login);
            cookie.Value = model.username;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
        }
        #endregion

        public ActionResult Logout()
        {
            if (Request.Cookies[CommonModel.login] != null)
            {
                Response.Cookies.Remove(CommonModel.login);
                Response.Cookies[CommonModel.login].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}