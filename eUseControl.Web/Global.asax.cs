using AutoMapper;
using eUseControl.Data.Entities.Admin;
using eUseControl.Data.Entities.User;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace eUseControl.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
           BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAutoMapper();
        }

        protected static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserLogin, ULoginData>();
                cfg.CreateMap<UserRegister, UregisterData>();
                cfg.CreateMap<UserTable, UserMinimal>();

                 cfg.CreateMap<AddUser, AddUserData>();
                 cfg.CreateMap<AddBook, AddBookData>();

                 cfg.CreateMap<EditUser, EditUserData>();
                 cfg.CreateMap<EditBook, EditBookData>();
            });
        }
    }
}