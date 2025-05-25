using eUseControl.BussinesLogic.Interfaces;
using eUseControl.Data.Entities.User;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using eUseControl.BussinesLogic;

namespace eUseControl.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }

        public object Mapper { get; private set; }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var data = AutoMapper.Mapper.Map<ULoginData>(login);

                data.LoginIp = Request.UserHostAddress;
                data.LoginDateTime = DateTime.Now;

                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
                }
            }

            return View();
        }
    }
}
