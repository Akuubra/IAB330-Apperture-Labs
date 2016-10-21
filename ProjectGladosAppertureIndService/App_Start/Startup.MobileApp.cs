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
using System.Data.Entity.Migrations;

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
            Database.SetInitializer(new MessageRequestStoreInitializer());

            Database.SetInitializer(new MessageResponseStoreInitializer());
            Database.SetInitializer(new UserFavouritesStoreInitializer());
            
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

            foreach (UserStore user in users)
            {
                context.Set<UserStore>().Add(user);
            }
            base.Seed(context);
        }
    }

    public class MessageRequestStoreInitializer : CreateDatabaseIfNotExists<MessageRequestStoreContext>
    {
        protected override void Seed(MessageRequestStoreContext context)
        {
            List<MessageRequestStore> messages = new List<MessageRequestStore>
            {
               new MessageRequestStore { Sender = "deraj", ReceivedBy = "Paul", Location = "Y", Meet = "Level 1", Time = "1pm" },
               new MessageRequestStore { Sender = "deraj",  ReceivedBy = "Jim", Location = "Y", Meet = "Level 1", Time = "1pm" },
               new MessageRequestStore { Sender = "deraj",  ReceivedBy = "Tom", Location = "Y", Meet = "Level 1", Time = "1pm"},

            };

            foreach (MessageRequestStore message in messages)
            {
                context.Set<MessageRequestStore>().Add(message);
            }
            base.Seed(context);
        }
    }
    public class MessageResponseStoreInitializer : CreateDatabaseIfNotExists<MessageResponseStoreContext>
    {
        protected override void Seed(MessageResponseStoreContext context)
        {
            List<MessageResponseStore> messages = new List<MessageResponseStore>
            {
               new MessageResponseStore { MessageID = "7cf9d110-8a97-473e-a11f-9aec7ab33995", Sender = "acd5a423-7cb1-4d00-8869-657dfb188998", Location = "Y" , Meet= "Y"},
               new MessageResponseStore { MessageID = "7cf9d110-8a97-473e-a11f-9aec7ab33995",  Sender = "ec98b409-cfb8-4ebe-9769-806db5b6f5cc", Location = "Y", Meet= "Y"},
               new MessageResponseStore { MessageID = "7cf9d110-8a97-473e-a11f-9aec7ab33995",  Sender = "ec98b409-cfb8-4ebe-9769-806db5b6f5cc", Location = "Y", Meet= "Y"},

            };

            foreach (MessageResponseStore message in messages)
            {
                context.Set<MessageResponseStore>().Add(message);
            }
            base.Seed(context);
        }
    }


    public class UserFavouritesStoreInitializer : CreateDatabaseIfNotExists<UserFavouritesStoreContext>
    {
        protected override void Seed(UserFavouritesStoreContext context)
        {
            List<UserFavouritesStore> messages = new List<UserFavouritesStore>
            {
               new UserFavouritesStore { UserID = "be90dbbf-a595-48a7-8b0c-0f1c60bef789", FavouriteUserID = "acd5a423-7cb1-4d00-8869-657dfb188998", isFavourite = true },
               new UserFavouritesStore { UserID = "be90dbbf-a595-48a7-8b0c-0f1c60bef789",  FavouriteUserID = "ec98b409-cfb8-4ebe-9769-806db5b6f5cc", isFavourite = true},
               new UserFavouritesStore { UserID = "6f595822-4552-4cf3-99cb-54cbf7c141f7",  FavouriteUserID = "ec98b409-cfb8-4ebe-9769-806db5b6f5cc", isFavourite = true},

            };

            foreach (UserFavouritesStore message in messages)
            {
                context.Set<UserFavouritesStore>().Add(message);
            }
            base.Seed(context);
        }
    }
}

