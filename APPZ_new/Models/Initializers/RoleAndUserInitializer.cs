﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using APPZ_new.Data;
using APPZ_new.Enums;
using APPZ_new.Models;

namespace APPZ_new.Models.Initializers
{
    public class RoleAndUserInitializer
    {
        private static string[] _passwords = { "admin", "ivanko" };
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var identityContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var mainContext = serviceProvider.GetRequiredService<AppDBContext>();

            var superAdmin = GetSuperAdmin();


            if (!identityContext.Users.Any(u => u.UserName == superAdmin.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(superAdmin, "Qw1234!");
                superAdmin.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(identityContext);
                var result = userStore.CreateAsync(superAdmin);

                mainContext.Users.Add(new User { Name = superAdmin.UserName, Role = UserRole.SuperAdmin });

            }
            identityContext.SaveChangesAsync();

            var admin = GetAdmin();

            if (!identityContext.Users.Any(u => u.UserName == admin.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(admin, _passwords[1]);
                admin.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(identityContext);
                var result = userStore.CreateAsync(admin);

                mainContext.Users.Add(new User { Name = admin.UserName, Role = UserRole.Admin });
            }

            identityContext.SaveChangesAsync();
        }


        private static ApplicationUser GetSuperAdmin()
        {
            return new ApplicationUser
            {
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
        }

        private static ApplicationUser GetAdmin()
        {
            return new ApplicationUser
            {
                Email = "admin1@mail.com",
                NormalizedEmail = "ADMIN1@MAIL.COM",
                UserName = "Ivan Administrator",
                NormalizedUserName = "IVAN ADMINISTRATOR",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
        }
    }
}
