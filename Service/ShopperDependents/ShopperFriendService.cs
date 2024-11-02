using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Entity.Profile;
using Domain.Repository.Base;
using Domain.Service.ShopperDependents;
using Microsoft.EntityFrameworkCore;

namespace Service.ShopperDependents;

public class ShopperFriendService : IShopperFriendService
{
    private readonly IMapper _mapper;
    private readonly IRepository<ShopperFriend> _repository;


    public async Task<ServiceResult<Dictionary<int, string>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.TableNoTracking.ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);
        return new ServiceResult<Dictionary<int, string>>(true, ApiResultStatusCode.Success, result);
    }
}