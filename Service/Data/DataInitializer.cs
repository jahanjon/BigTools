using Domain.Entity.Category;
using Domain.Entity.Identity;
using Domain.Repository.Base;
using Domain.Service.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Service.Data;

public class DataInitializer : IDataInitializer
{
    private readonly IRepository<CategoryLevel1> _categoryLevel1Repository;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public DataInitializer(RoleManager<Role> roleManager, UserManager<User> userManager,
        IRepository<CategoryLevel1> categoryLevel1Repository)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _categoryLevel1Repository = categoryLevel1Repository;
    }

    public void InitializeData()
    {
        InitRolesAsync().Wait();
        InitAdminAsync().Wait();
        InitCategoriesAsync().Wait();
    }

    private async Task InitRolesAsync()
    {
        var adminRole = new Role
        {
            Name = "Admin"
        };
        if (!await _roleManager.Roles.AnyAsync(r => r.Name == adminRole.Name))
        {
            await _roleManager.CreateAsync(adminRole);
        }

        var shopperRole = new Role
        {
            Name = "Shopper"
        };
        if (!await _roleManager.Roles.AnyAsync(r => r.Name == shopperRole.Name))
        {
            await _roleManager.CreateAsync(shopperRole);
        }

        var supplierRole = new Role
        {
            Name = "Supplier"
        };
        if (!await _roleManager.Roles.AnyAsync(r => r.Name == supplierRole.Name))
        {
            await _roleManager.CreateAsync(supplierRole);
        }
    }

    private async Task InitAdminAsync()
    {
        var admin = new User
        {
            Mobile = "09304359576",
            UserName = "admin1",
            Email = "elias.taghavi.rz@outlook.com",
            EmailConfirmed = true,
            IsActive = true
        };
        if (!await _userManager.Users.AnyAsync(u => u.UserName == admin.UserName))
        {
            await _userManager.CreateAsync(admin, "BigTools0218");
        }
        else
        {
            admin = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == admin.UserName);
        }

        if (!await _userManager.IsInRoleAsync(admin, "Admin"))
        {
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
    }


    private async Task InitCategoriesAsync()
    {
        if (await _categoryLevel1Repository.TableNoTracking.AnyAsync())
        {
            return;
        }

        var categories = new List<CategoryLevel1>
        {
            new()
            {
                Code = "11",
                Name = "ابزار برقی و شارژی",
                ChildCategories = new List<CategoryLevel2>
                {
                    new()
                    {
                        Code = "1111",
                        Name = "دریل",
                        ChildCategories = new List<CategoryLevel3>
                        {
                            new()
                            {
                                Code = "111112",
                                Name = "دریل شارژی"
                            },
                            new()
                            {
                                Code = "111113",
                                Name = "دریل گیربکسی"
                            },
                            new()
                            {
                                Code = "111114",
                                Name = "دریل چکشی"
                            },
                            new()
                            {
                                Code = "111115",
                                Name = "دریل ستونی"
                            },
                            new()
                            {
                                Code = "111116",
                                Name = "دریل نمونه بردار"
                            }
                        }
                    },
                    new()
                    {
                        Code = "1112",
                        Name = "اره"
                    },
                    new()
                    {
                        Code = "1113",
                        Name = "فرز"
                    },
                    new()
                    {
                        Code = "1114",
                        Name = "بتن کن و چکش تخریب"
                    },
                    new()
                    {
                        Code = "1115",
                        Name = "کارواش"
                    },
                    new()
                    {
                        Code = "1116",
                        Name = "سایر"
                    }
                }
            },
            new()
            {
                Code = "22",
                Name = "ابزار دستی"
            },
            new()
            {
                Code = "33",
                Name = "ابزار مصرفی و جانبی"
            },
            new()
            {
                Code = "44",
                Name = "ابزار و تجهیزات جانبی"
            },
            new()
            {
                Code = "55",
                Name = "ابزار بادی و بنزینی"
            },
            new()
            {
                Code = "66",
                Name = "قفل و یراق آلات"
            },
            new()
            {
                Code = "77",
                Name = "لوازم بارگیری و حفاری"
            },
            new()
            {
                Code = "88",
                Name = "ابزار ساختمانی و بهداشتی"
            },
            new()
            {
                Code = "99",
                Name = "ابزار تخصصی"
            }
        };

        _categoryLevel1Repository.AddRange(categories);
    }
}