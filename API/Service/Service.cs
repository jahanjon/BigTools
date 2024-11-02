using Domain.Service;
using Domain.Service.Category;
using Domain.Service.Communication;
using Domain.Service.Data;
using Domain.Service.Financial;
using Domain.Service.Identity;
using Domain.Service.LandingSearch;
using Domain.Service.Place;
using Domain.Service.Product;
using Domain.Service.Profile;
using Domain.Service.ShopperDependents;
using Service;
using Service.Category;
using Service.Communication;
using Service.Data;
using Service.Financial;
using Service.Identity;
using Service.LandingSearch;
using Service.Place;
using Service.Product;
using Service.Profile;
using Service.ShopperDependents;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddService(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDataInitializer, DataInitializer>();
        builder.Services.AddTransient<IJwtService, JwtService>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<ILoginCodeService, LoginCodeService>();
        builder.Services.AddTransient<IProvinceService, ProvinceService>();
        builder.Services.AddTransient<ICityService, CityService>();
        builder.Services.AddTransient<ISupplierService, SupplierService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IBrandService, BrandService>();
        builder.Services.AddTransient<IGoodService, GoodService>();
        builder.Services.AddTransient<IUnitService, UnitService>();
        builder.Services.AddTransient<IPackageTypeService, PackageTypeService>();
        builder.Services.AddTransient<IShopperSevice, ShopperService>();
        builder.Services.AddTransient<IShopperFriendService, ShopperFriendService>();
        builder.Services.AddTransient<IGoodDiscountService, GoodDiscountService>();
        builder.Services.AddTransient<ILandingSearchService, LandingSearchService>();
        builder.Services.AddTransient<IMessageSendService, MessageSendService>();
        builder.Services.AddTransient<IMessageService, MessageService>();
        builder.Services.AddTransient<IRelationshipService, RelationshipService>();
    }
}