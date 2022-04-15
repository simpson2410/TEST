using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DAL
{
    public class AccountInitialize : IAccountInitialize
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountInitialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");
            var operatorRole = new IdentityRole("Operator");
            if (!_roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>() { adminRole, userRole };
                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
            }
            var master = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                FullName = "Admin",
                EmailConfirmed = true
            };
            if (!_userManager.Users.Any(x=>x.UserName == master.UserName))
            {
                _userManager.CreateAsync(master, "Admin1@34").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(master, adminRole.Name).GetAwaiter().GetResult();
            }

            if (_userManager.Users.Any()) return;


            
            var staffUser = new ApplicationUser()
            {
                UserName = "staff1",
                Email = "staff1@gmail.com",
                FullName = "staff nguyễn",
                EmailConfirmed = true
            };
          
            _userManager.CreateAsync(staffUser, "Admin1@34").GetAwaiter().GetResult();
       
            _userManager.AddToRoleAsync(staffUser, userRole.Name).GetAwaiter().GetResult();
          

        }
        public void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<TNRContext>();
                context.Database.EnsureCreated();

                var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = _roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Result;
                }
                if (!_roleManager.RoleExistsAsync("User").Result)
                {
                    var role = _roleManager.CreateAsync(new IdentityRole { Name = "User" }).Result;
                }

            
                var permissions = GetPermissions();
                var permissionMenus = GetPermissions();
                foreach (var item in permissionMenus)
                {
                    if (!context.NavigationMenu.Any(n => n.Name == item.Name))
                    {

                        context.NavigationMenu.Add(item);

                    }
                }
                context.SaveChanges();
               
                var _adminRole = _roleManager.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
                foreach (var menurole in permissions)
                {
                    if (!context.RoleMenuPermission.Any(x => x.RoleId == _adminRole.Id && x.NavigationMenuId == menurole.Id))
                    {
                        context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _adminRole.Id, NavigationMenuId = menurole.Id });
                    }

                }

                var _userRole = _roleManager.Roles.Where(x => x.Name == "User").FirstOrDefault();
                foreach (var menurole in permissions.Where(x=>x.Id != new Guid("B30D583A-F7A6-43C3-B54A-0AD2A4952E55")))
                {
                    if (!context.RoleMenuPermission.Any(x => x.RoleId == _userRole.Id && x.NavigationMenuId == menurole.Id))
                    {
                        context.RoleMenuPermission.Add(new RoleMenuPermission() { RoleId = _userRole.Id, NavigationMenuId = menurole.Id });
                    }

                }

                context.SaveChanges();
            }
        }
        private static List<NavigationMenu> GetPermissions()
        {
            return new List<NavigationMenu>()
            {
                new NavigationMenu()
                {
                    Id = new Guid("51DD7D4C-81F6-41B1-A7C0-50D0A18A8EEE"),
                    Name = "Dashboard",
                    Area= "Admins",
                    ControllerName = "Home",
                    ActionName = "Index",
                    ParentMenuId = null,
                    DisplayOrder = 1,
                    Visible = false,
                    IsMenu = true,
                    Image = "fe fe-home",
                    IsAdmin = true
                },
                  new NavigationMenu()
                {
                    Id = new Guid("D4E4610D-4860-46D0-A175-2C6C8155CD38"),
                    Name = "Danh sách đăng ký",
                    Area= "Admins",
                    ControllerName = "RegisterInformation",
                    ActionName = "Index",
                    ParentMenuId = null,
                    DisplayOrder = 4,
                    Visible = false,
                    IsMenu = true,
                    Image = "fe fe-layout",
                    IsAdmin = true
                }/*,
                   new NavigationMenu()
                {
                    Id = new Guid("2BBA4789-A979-494E-A3CF-C5EE26F23869"),
                    Name = "Danh sách trúng thưởng",
                    Area= "Admins",
                    ControllerName = "Award",
                    ActionName = "Index",
                    ParentMenuId = null,
                    DisplayOrder = 7,
                    Visible = false,
                    IsMenu = true,
                    Image = "fe fe-layout",
                    IsAdmin = true
                }*/
            };
        }
    }
    public interface IAccountInitialize
    {
        Task SeedData();
        void Initialize(IApplicationBuilder app);
    }
}
