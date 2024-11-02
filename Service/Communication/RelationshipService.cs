using System.Linq.Dynamic.Core;
using AutoMapper;
using Common.Enums;
using Domain;
using Domain.Dto.Common;
using Domain.Dto.Communication;
using Domain.Entity.Communication;
using Domain.Repository.Base;
using Domain.Repository.Identity;
using Domain.Repository.Profile;
using Domain.Service.Communication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Service.Communication;

public class RelationshipService : IRelationshipService
{
    private readonly IRepository<RelationshipHistory> _historyRepository;
    private readonly IStringLocalizer<RelationshipService> _localizer;
    private readonly IMapper _mapper;
    private readonly IRepository<Relationship> _repository;
    private readonly IShopperRepository _shopperRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUserRepository _userRepository;

    public RelationshipService(
        IRepository<Relationship> repository,
        IRepository<RelationshipHistory> historyRepository,
        ISupplierRepository supplierRepository,
        IUserRepository userRepository,
        IShopperRepository shopperRepository,
        IStringLocalizer<RelationshipService> localizer,
        IMapper mapper)
    {
        _repository = repository;
        _historyRepository = historyRepository;
        _supplierRepository = supplierRepository;
        _userRepository = userRepository;
        _shopperRepository = shopperRepository;
        _localizer = localizer;
        _mapper = mapper;
    }



    public async Task<ServiceResult> CreateAsync(RelationshipCreateDto dto, int userId, CancellationToken cancellationToken)
    {
        var relationshipExists = await _repository.TableNoTracking
            .Where(r => r.RequesterId == dto.RequesterId && r.RequesterType == dto.RequesterType
                                                         && (r.AcceptorId == dto.AcceptorId.Value || r.AcceptorPhoneNumber == dto.AcceptorPhoneNumber) && r.AcceptorType == dto.AcceptorType
                                                         && r.RelationshipType == dto.RelationshipType
                        || (r.RequesterId == dto.AcceptorId || r.RequesterPhoneNumber == dto.AcceptorPhoneNumber) && r.RequesterType == dto.AcceptorType
                                                                                                                  && r.AcceptorId == dto.RequesterId && r.AcceptorType == dto.RequesterType
                                                                                                                  && r.RelationshipType == dto.RelationshipType)
            .AnyAsync();

        if (relationshipExists)
        {
            return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["RelationshipExists"]);
        }

        var relationship = new Relationship
        {
            RelationshipType = dto.RelationshipType,
            Status = RelationshipState.NotAccepted,
            RequesterType = dto.RequesterType,
            AcceptorType = dto.AcceptorType,
            Description = dto.Description
        };

        //Requester

        if (dto.RequesterType == RelationshipMemberType.Supplier)
        {
            var supplier = await _supplierRepository.TableNoTracking
                .Where(s => s.Id == dto.RequesterId).SingleOrDefaultAsync();
            if (supplier is null)
            {
                return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["RequesterNotFound"]);
            }

            if (supplier.UserId != userId)
            {
                return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, _localizer["UnAuthorized"]);
            }

            relationship.RequesterId = dto.RequesterId;
            relationship.RequesterName = supplier.Name;
            relationship.RequesterCityId = supplier.CityId;
            relationship.RequesterPhoneNumber = supplier.Mobile;
            relationship.RequesterNationalId = supplier.NationalId;
        }
        else if (dto.RequesterType == RelationshipMemberType.Shopper)
        {
            var shopper = await _shopperRepository.TableNoTracking
                .Where(sh => sh.Id == dto.RequesterId).SingleOrDefaultAsync();
            if (shopper is null)
            {
                return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["RequesterNotFound"]);
            }

            if (shopper.UserId != userId)
            {
                return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, _localizer["UnAuthorized"]);
            }

            relationship.RequesterId = dto.RequesterId;
            relationship.RequesterName = shopper.Name;
            relationship.RequesterCityId = shopper.CityId;
            relationship.RequesterPhoneNumber = shopper.Mobile;
        }
        else
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["IncorrectRequesterType"]);
        }

        //Acceptor

        if (dto.AcceptorType == RelationshipMemberType.Supplier)
        {
            if (dto.AcceptorExists)
            {
                //var id = s
                var supplier = await _supplierRepository.TableNoTracking
                    .Where(s => s.Id == dto.AcceptorId).SingleOrDefaultAsync();
                if (supplier is null)
                {
                    return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["AcceptorNotFound"]);
                }

                relationship.AcceptorId = dto.AcceptorId;
                relationship.AcceptorName = supplier.Name;
                relationship.AcceptorCityId = supplier.CityId;
                relationship.AcceptorPhoneNumber = supplier.Mobile;
                relationship.AcceptorNationalId = supplier.NationalId;
            }
            else
            {
                if (string.IsNullOrEmpty(dto.AcceptorPhoneNumber) || string.IsNullOrEmpty(dto.AcceptorName) || dto.AcceptorCityId == null)
                {
                    return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["EnterAcceptorDetails"]);
                }

                relationship.AcceptorName = dto.AcceptorName;
                relationship.AcceptorCityId = dto.AcceptorCityId.Value;
                relationship.AcceptorPhoneNumber = dto.AcceptorPhoneNumber;
                relationship.AcceptorNationalId = dto.AcceptorNationalId;
                //todo: send invite sms
                relationship.Status = RelationshipState.Invited;
            }
        }
        else if (dto.AcceptorType == RelationshipMemberType.Shopper)
        {
            if (dto.AcceptorExists)
            {
                var shopper = await _shopperRepository.TableNoTracking
                    .Where(sh => sh.Id == dto.AcceptorId).SingleOrDefaultAsync();
                if (shopper is null)
                {
                    return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["AcceptorNotFound"]);
                }

                relationship.AcceptorId = dto.AcceptorId;
                relationship.AcceptorName = shopper.Name;
                relationship.AcceptorCityId = shopper.CityId;
                relationship.AcceptorPhoneNumber = shopper.Mobile;
                //todo: NationalId??
            }
            else
            {
                if (string.IsNullOrEmpty(dto.AcceptorPhoneNumber) || string.IsNullOrEmpty(dto.AcceptorName) || dto.AcceptorCityId == null)
                {
                    return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["EnterAcceptorDetails"]);
                }

                relationship.AcceptorName = dto.AcceptorName;
                relationship.AcceptorCityId = dto.AcceptorCityId.Value;
                relationship.AcceptorPhoneNumber = dto.AcceptorPhoneNumber;
                //todo: NationalId??
                //todo: send invite sms
                relationship.Status = RelationshipState.Invited;
            }
        }
        else if (dto.AcceptorType == RelationshipMemberType.Repairman)
        {
            if (string.IsNullOrEmpty(dto.AcceptorPhoneNumber) || string.IsNullOrEmpty(dto.AcceptorName) || dto.AcceptorCityId == null)
            {
                return new ServiceResult(false, ApiResultStatusCode.BadRequest, _localizer["EnterAcceptorDetails"]);
            }

            relationship.AcceptorName = dto.AcceptorName;
            relationship.AcceptorCityId = dto.AcceptorCityId.Value;
            relationship.AcceptorPhoneNumber = dto.AcceptorPhoneNumber;
            relationship.AcceptorNationalId = dto.AcceptorNationalId;
        }
        else
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["IncorrectRequesterType"]);
        }

        var history = new List<RelationshipHistory>
        {
            new()
            {
                CreatedAt = DateTime.UtcNow,
                PerformerType = relationship.RequesterType,
                PerformerId = relationship.RequesterId,
                NewState = RelationshipState.NotAccepted,
                Description = ""
            }
        };

        relationship.History = history;

        await _repository.AddAsync(relationship, cancellationToken);

        return new ServiceResult(true, ApiResultStatusCode.Success, _localizer["RelationshipRequestCreated"]);
    }



    public async Task<ServiceResult> AnswerAsync(RelationshipAnswerDto dto, int userId, CancellationToken cancellationToken)
    {
        //todo : check authorized user
        var relationship = await _repository.Table
            .Where(r => r.Id == dto.Id).Include(r => r.History).SingleOrDefaultAsync();
        if (relationship == null)
        {
            return new ServiceResult(false, ApiResultStatusCode.NotFound, _localizer["RelationshipRequestNotFound"]);
        }

        if (relationship.AcceptorType == RelationshipMemberType.Supplier)
        {
            var supplier = await _supplierRepository.GetByIdAsync(cancellationToken, relationship.AcceptorId);
            if (supplier.UserId != userId)
            {
                return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, _localizer["UnAuthorized"]);
            }
        }
        else if (relationship.AcceptorType == RelationshipMemberType.Shopper)
        {
            var shopper = await _shopperRepository.GetByIdAsync(cancellationToken, relationship.AcceptorId);
            if (shopper.UserId != userId)
            {
                return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, _localizer["UnAuthorized"]);
            }
        }

        if (relationship.AcceptorId != dto.AcceptorId)
        {
            return new ServiceResult(false, ApiResultStatusCode.UnAuthorized, _localizer["UnAuthorized"]);
        }

        if (dto.IsAccepted)
        {
            relationship.Status = RelationshipState.Accepted;
            relationship.History.Add(new RelationshipHistory
            {
                CreatedAt = DateTime.UtcNow,
                PerformerType = relationship.AcceptorType,
                PerformerId = relationship.AcceptorId.Value,
                NewState = RelationshipState.Accepted
            });
        }
        else
        {
            relationship.Status = RelationshipState.Cancelled;
            relationship.History.Add(new RelationshipHistory
            {
                CreatedAt = DateTime.UtcNow,
                PerformerType = relationship.AcceptorType,
                PerformerId = relationship.AcceptorId.Value,
                NewState = RelationshipState.Cancelled
            });
        }

        if (relationship.RelationshipType == RelationshipType.Colleague)
        {
            relationship.HasKnown = dto.HasKnown;
            relationship.HasKnownFromYear = dto.HasKnownFromYear;
            relationship.HasTrade = dto.HasTrade;
            relationship.HasTradeFromYear = dto.HasTradeFromYear;
            relationship.Suggests = dto.Suggests;
        }

        await _repository.UpdateAsync(relationship, cancellationToken);

        return new ServiceResult(true, ApiResultStatusCode.Success, _localizer["RelationshipStatusUpdated"]);
    }


    public async Task<ServiceResult<PagedListDto<RelationshipListDto>>> GetListAsync(RequestedPageDto<RelationshipFilterDto> dto, CancellationToken cancellationToken)
    {
        var query = _repository.TableNoTracking
            .Where(r => r.RequesterId == dto.Filter.CurrentMemberId && r.RequesterType == dto.Filter.CurrentMemberType
                        || r.AcceptorId == dto.Filter.CurrentMemberId && r.AcceptorType == dto.Filter.CurrentMemberType);

        //todo: apply filters

        var selectResult = query.Select(r => new RelationshipListDto
        {
            Id = r.Id,
            RequesterId = r.RequesterId,
            RequesterName = r.RequesterName,
            RequesterType = r.RequesterType,
            RequesterCityId = r.RequesterCityId,
            RequesterPhoneNumber = r.RequesterPhoneNumber,
            RequesterNationalId = r.RequesterNationalId,
            AcceptorId = r.AcceptorId,
            AcceptorName = r.AcceptorName,
            AcceptorType = r.AcceptorType,
            AcceptorCityId = r.AcceptorCityId,
            AcceptorPhoneNumber = r.AcceptorPhoneNumber,
            AcceptorNationalId = r.AcceptorNationalId,
            RelationshipType = r.RelationshipType,
            Status = r.Status,
            Description = r.Description,
            IsNew = r.AcceptorId == dto.Filter.CurrentMemberId && r.AcceptorType == dto.Filter.CurrentMemberType
                                                               && r.History.Count == 1
        });

        selectResult = string.IsNullOrEmpty(dto.OrderPropertyName)
            ? selectResult.OrderBy(x => x.Id)
            : (IQueryable<RelationshipListDto>)selectResult.OrderBy($"{dto.OrderPropertyName} {dto.OrderType}");

        var count = await selectResult.CountAsync(cancellationToken);
        var data = await selectResult.Skip(dto.PageSize * (dto.PageIndex - 1)).Take(dto.PageSize).ToListAsync();

        return new ServiceResult<PagedListDto<RelationshipListDto>>(new PagedListDto<RelationshipListDto>
        {
            Data = data,
            Count = count
        });

    }
}