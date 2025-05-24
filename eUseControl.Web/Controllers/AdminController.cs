using eUseControl.BussinesLogic.Interfaces;
using eUseControl.BussinesLogic;
using eUseControl.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Web.Models;
using AutoMapper;
using eUseControl.Data.Entities.Admin;
using eUseControl.BussinesLogic.AppBL;
using eUseControl.Data.Entities.User;
using eUseControl.Data.Entities.Product;
using System.IO;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace eUseControl.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ISession _session;
        private readonly IAdmin _admin;
        List<string> TypeList = new List<string> { "Poezie", "Autori români", "Istorie", "Dezvoltare personală", "Ficțiune" };

        public AdminController()
        {
            var bl = new BussinessLogic();
            _session = bl.GetSessionBL();
            _admin = bl.GetAdminBL();
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

        public ActionResult AddUser()
        {
            GetHeaderData();
            List<UserTable> usersList = _session.GetUsersList().OrderByDescending(u => u.Level).ToList();
            ViewBag.usersList = usersList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUser user, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<AddUserData>(user);

                if (imageFile != null && imageFile.ContentType == "image/png")
                {
                    using (var db = new TableContext())
                    {
                        var path = Path.Combine(Server.MapPath($"~/assets/images/users/{data.Username}.png"));
                        imageFile.SaveAs(path);
                    }
                }

                var addUser = _admin.AddUser(data);
                if (addUser.Status)
                {
                    return RedirectToAction("AddUser", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", addUser.StatusMsg);
                    return View();
                }
            }
            return View();
        }

        public ActionResult DeleteUser(int id)
        {
            using (var db = new TableContext())
            {
                UserTable user = db.Users.FirstOrDefault(u => u.Id == id);
                var path = Path.Combine(Server.MapPath($"~/assets/images/users/{user.Image}"));
                System.IO.File.Delete(path);
            }
            _admin.DeleteUser(id);
            return RedirectToAction("AddUser", "Admin");
        }

        public ActionResult EditUser(int id)
        {
            GetHeaderData();
            using (var db = new TableContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                var data = Mapper.Map<EditUser>(user);

                ViewBag.user = data;
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUser user, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentType == "image/png")
                {
                    using (var db = new TableContext())
                    {
                        UserTable existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                        var path = Path.Combine(Server.MapPath($"~/assets/images/users/{existingUser.Username}.png"));
                        existingUser.Image = existingUser.Username + ".png";
                        db.SaveChanges();
                        System.IO.File.Delete(path);
                        imageFile.SaveAs(path);
                    }
                }
                var data = Mapper.Map<EditUserData>(user);

                var editUser = _admin.EditUser(data);
                if (editUser.Status)
                {
                    return RedirectToAction("AddUser", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", editUser.StatusMsg);
                    return View();
                }
            }
            return View();
        }
        public ActionResult AddBook()
        {
            GetHeaderData();
            List<BookTable> booksList = _session.GetBooksList();
            ViewBag.booksList = booksList;
            ViewBag.TypeList = TypeList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(AddBook book, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<AddBookData>(book);

                if (imageFile != null && imageFile.ContentType == "image/png")
                {
                    using (var db = new TableContext())
                    {
                        var path = Path.Combine(Server.MapPath($"~/assets/images/books/{data.Name}.png"));
                        imageFile.SaveAs(path);
                    }
                }

                ViewBag.TypeList = TypeList;

                var addBook = _admin.AddBook(data);
                if (addBook.Status)
                {
                    return RedirectToAction("AddBook", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", addBook.StatusMsg);
                    return View();
                }
            }
            return View();
        }

        public ActionResult DeleteBook(int id)
        {
            using (var db = new TableContext())
            {
                BookTable book = db.Books.FirstOrDefault(u => u.Id == id);
                var path = Path.Combine(Server.MapPath($"~/assets/images/books/{book.Image}"));
                System.IO.File.Delete(path);
            }
            _admin.DeleteBook(id);
            return RedirectToAction("AddBook", "Admin");
        }

        public ActionResult EditBook(int id)
        {
            GetHeaderData();
            using (var db = new TableContext())
            {
                var book = db.Books.FirstOrDefault(u => u.Id == id);
                var data = Mapper.Map<EditBook>(book);

                ViewBag.TypeList = TypeList;
                ViewBag.book = book;
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(EditBook book, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentType == "image/png")
                {
                    using (var db = new TableContext())
                    {
                        BookTable existingBook = db.Books.FirstOrDefault(u => u.Name == book.Name);
                        var path = Path.Combine(Server.MapPath($"~/assets/images/books/{existingBook.Name}.png"));
                        existingBook.Image = existingBook.Name + ".png";
                        db.SaveChanges();
                        System.IO.File.Delete(path);
                        imageFile.SaveAs(path);
                    }
                }
                ViewBag.TypeList = TypeList;

                var data = Mapper.Map<EditBookData>(book);

                var editBook = _admin.EditBook(data);
                if (editBook.Status)
                {
                    return RedirectToAction("AddBook", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", editBook.StatusMsg);
                    return View();
                }
            }
            return View();
        }
    }
}
