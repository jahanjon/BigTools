using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Expressions;
using Common.Exceptions;
using Domain.Dto.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Extensions;

public static class EfExtensions
{
    public static async Task<PagedListDto<T>> ToPagedListDtoAsync<T>(this IQueryable<T> query, RequestedPageDto filterDto, CancellationToken cancellationToken = default)
    {
        if (filterDto.PageIndex <= 0)
        {
            throw new RepositoryException("Page index must be greater than 0");
        }
        if (filterDto.PageSize is <= 0 or > 100)
        {
            throw new RepositoryException("Page size must be between 1 and 100");
        }
        if (string.IsNullOrEmpty(filterDto.OrderPropertyName))
        {
            filterDto.OrderPropertyName = "Id";
        }
        try
        {
            query = query.OrderBy($"{filterDto.OrderPropertyName} {filterDto.OrderType}");
        }
        catch (ParseException e)
        {
            throw new RepositoryException("Invalid order property name");
        }

        var count = await query.CountAsync(cancellationToken);
        var data = await query
            .Skip(filterDto.PageSize * (filterDto.PageIndex - 1))
            .Take(filterDto.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedListDto<T>
        {
            Data = data,
            Count = count
        };
    }

    public static async Task<PagedListDto<TResult>> ToPagedListDtoAsync<TSource, TResult>(this IQueryable<TSource> query, RequestedPageDto filterDto, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken = default)
    {
        if (filterDto.PageIndex <= 0)
        {
            throw new RepositoryException("Page index must be greater than 0");
        }
        if (filterDto.PageSize is <= 0 or > 100)
        {
            throw new RepositoryException("Page size must be between 1 and 100");
        }
        if (string.IsNullOrEmpty(filterDto.OrderPropertyName))
        {
            filterDto.OrderPropertyName = "Id";
        }
        try
        {
            query = query.OrderBy($"{filterDto.OrderPropertyName} {filterDto.OrderType}");
        }
        catch (ParseException e)
        {
            throw new RepositoryException("Invalid order property name");
        }

        var count = await query.CountAsync(cancellationToken);
        var data = await query
            .Select(selector)
            .Skip(filterDto.PageSize * (filterDto.PageIndex - 1))
            .Take(filterDto.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedListDto<TResult>
        {
            Data = data,
            Count = count
        };
    }
}