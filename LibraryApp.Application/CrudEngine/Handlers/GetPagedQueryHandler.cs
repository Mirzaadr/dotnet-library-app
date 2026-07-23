// Application/CrudEngine/Handlers/GetPagedQueryHandler.cs
// using Application.Common;
using LibraryApp.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
// using System.Linq.Dynamic.Core;

namespace LibraryApp.Application.CrudEngine.Handlers;

public class GetPagedQueryHandler<TEntity, TId> : IRequestHandler<GetPagedQuery<TEntity, TId>, Result<PagedResult<TEntity>>>
    where TEntity : Entity<TId>
    where TId : notnull
{
    private readonly IAppDBContext _db;

    public GetPagedQueryHandler(IAppDBContext db) => _db = db;

    public async Task<Result<PagedResult<TEntity>>> Handle(GetPagedQuery<TEntity, TId> request, CancellationToken ct)
    {
        var query = _db.Set<TEntity>().AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            var direction = request.Descending ? "descending" : "ascending";
            // query = query.OrderBy($"{request.SortBy} {direction}");
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(ct);

        return Result<PagedResult<TEntity>>.Success(new PagedResult<TEntity>(items, totalCount, request.Page, request.PageSize));
    }
}