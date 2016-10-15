using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using ProjectGladosAppertureIndService.DataObjects;
using ProjectGladosAppertureIndService.Models;
using Owin;

namespace ProjectGladosAppertureIndService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new ProjectGladosAppertureIndInitializer());
            Database.SetInitializer(new UserStoreInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<ProjectGladosAppertureIndContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class ProjectGladosAppertureIndInitializer : CreateDatabaseIfNotExists<ProjectGladosAppertureIndContext>
    {
        protected override void Seed(ProjectGladosAppertureIndContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }

    public class UserStoreInitializer : CreateDatabaseIfNotExists<UserStoreContext>
    {
        protected override void Seed(UserStoreContext context)
        {
            List<UserStore> users = new List<UserStore>
            {
               new UserStore { Username = "deraj", Email = "jaredsbagnall@gmail.com", First_Name = "Jared", Last_Name = "Bagnall" , Location = "Level 12", Password = "password" },
               new UserStore { Username = "sadf", Email = "jarasdfgnall@gmail.com", First_Name = "Paul", Last_Name = "pail" , Location = "Level 13", Password = "password1" },
               new UserStore { Username = "asdf", Email = "jareasdfadsbagnall@gmail.com", First_Name = "Frank", Last_Name = "far" , Location = "Level 13", Password = "password2" },

            };

            foreach (UserStore todoItem in users)
            {
                context.Set<UserStore>().Add(todoItem);
            }
            base.Seed(context);
        }
    }
}

