﻿using Bidme.Dominio.DataAccess;
using Bidme.Dominio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidme.MVC.Web.App_Start
{
    public class IdentityConfig
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/user/login")
            });
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<User>(
                new UserStore<User>(new UserContext()));
                // permite caracteres alfa numéricos no username
                usermanager.UserValidator = new UserValidator<User>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                return usermanager;
            };
        }
    }
}