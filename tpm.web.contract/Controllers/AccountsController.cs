using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using tpm.business.Helpers;
using tpm.business;
using tpm.dto.admin;
using tpm.web.contract.Models;
using System.Net;

namespace tpm.web.contract.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly UserRepository userRepository;

        public AccountsController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        

        public ActionResult Login()
        {
            var user = SessionHelper.Get<UserPrincipal>(HttpContext.Session, SessionKeys.CurrentUser);
            if (user != null && user.UserID > 0)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserPrincipal obj)
        {
            try
            {
                string isValidLogin = userRepository.ValidateLogin(obj);
                if (isValidLogin == "Success")
                {
                    // Lưu thông tin người dùng vào session
                    SessionHelper.Set(HttpContext.Session, SessionKeys.CurrentUser, obj);

                    var url = HttpContext.Request.Cookies["returnUrl"] ?? string.Empty;

                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Thông báo lỗi đăng nhập không hợp lệ
                    ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không chính xác.");
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }





        public ActionResult Logout()
        {
            //var userPricinpal = SessionHelper.Get<UserPrincipal>(HttpContext.Session, SessionKeys.UserPricinpal);
            //if (userPricinpal == null)
            //{
            //    return Redirect("/");
            //}

            //var idtoken = userPricinpal.Token;
            //HttpContext.Session.Remove(SessionKeys.UserPricinpal);

            //var idsEndPoint = AppCoreConfig.URLConnection.IDSUrl;
            //var redirectUrl = AppConfig.URLConnection.ClientUrl;

            //var url = string.Format(@"{0}/connect/endsession?post_logout_redirect_uri={1}&id_token_hint={2}&state={3}",
            //    idsEndPoint, redirectUrl, WebUtility.UrlDecode(idtoken),
            //    Guid.NewGuid().ToString("N"));

            SessionHelper.Remove(HttpContext.Session, SessionKeys.CurrentUser);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [MvcAuthorize(AuthorizeAction: false)]
        public ActionResult ReLogin()
        {
            //ViewBag.UserName = CurrentUser.Username;
            return View();
        }
    }
}
