using DataAccess.Base;
using DataAccess.Category;
using DataAccess.Communication;
using DataAccess.Identity;
using DataAccess.Place;
using DataAccess.Product;
using DataAccess.Profile;
using Domain.Repository.Base;
using Domain.Repository.Category;
using Domain.Repository.Communication;
using Domain.Repository.Identity;
using Domain.Repository.Place;
using Domain.Repository.Product;
using Domain.Repository.Profile;
using Domain.ViewRepository.Base;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddRepository(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddTransient(typeof(IViewRepository<>), typeof(ViewRepository<>));
        builder.Services.AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ILoginCodeRepository, LoginCodeRepository>();
        builder.Services.AddTransient<IUnitRepository, UnitRepository>();
        builder.Services.AddTransient<IPackageTypeRepository, PackageTypeRepository>();
        builder.Services.AddTransient<IBrandRepository, BrandRepository>();
        builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
        builder.Services.AddTransient<IShopperRepository, ShopperRepository>();
        builder.Services.AddTransient<IMessageRepository, MessageRepository>();
        builder.Services.AddTransient<ICategoryLevel1Repository, CategoryLevel1Repository>();
        builder.Services.AddTransient<ICityRepository, CityRepository>();
        builder.Services.AddTransient<IProvinceRepository, ProvinceRepository>();
    }
}