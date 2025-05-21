using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class HomeController : Controller
    {
        public void GetHeaderData()
        {
            SessionStatus();
            var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
            string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
            if (userStatus != "guest")
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                ViewBag.user = profile;
            }
            ViewBag.userStatus = userStatus;
        }
        // GET: Home
        public ActionResult Index()
        {
            GetHeaderData();
            var bestsellerBooks = _session.GetBooksList().OrderByDescending(f => f.Bought).Take(4);
            var cheapBooks = _session.GetBooksList().OrderBy(f => f.Price).Take(6);
            ViewBag.bestsellerBooks = bestsellerBooks;
            ViewBag.cheapBooks = cheapBooks;
            return View();
        }
    }
}