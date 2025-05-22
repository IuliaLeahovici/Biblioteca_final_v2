using eUseControl.BussinesLogic.Interfaces;
using eUseControl.BussinesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Data.Entities.Product;

namespace eUseControl.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISession _session;
        public HomeController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
        }
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
        public ActionResult Index()
        {
            GetHeaderData();
            var bestsellerBooks = _session.GetBooksList().OrderByDescending(f => f.Bought).Take(4);
            var cheapBooks = _session.GetBooksList().OrderBy(f => f.Price).Take(6);
            ViewBag.bestsellerBooks = bestsellerBooks;
            ViewBag.cheapBooks = cheapBooks;
            return View();
        }
        public ActionResult Product_details(string name)
        {
            GetHeaderData();
            var book = _session.GetBooksList().FirstOrDefault(f => f.Name == name);
            var booksList = _session.GetBooksList().Where(f => f.Type == book.Type).ToList().Take(5);

            ViewBag.book = book;
            ViewBag.booksList = booksList;
            return View();
        }
        public ActionResult Shop(string type)
        {
            GetHeaderData();
            List<BookTable> booksList;
            if (type == "All")
            {
                booksList = _session.GetBooksList();
            }
            else
            {
                booksList = _session.GetBooksList().Where(f => f.Type == type).ToList();
            }
            ViewBag.booksList = booksList;
            return View();
        }
    }
}