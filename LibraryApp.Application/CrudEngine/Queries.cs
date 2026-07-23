using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.CrudEngine;

public record GetByIdQuery<TEntity, TId>(TId Id) : IRequest<Result<TEntity>>
    where TEntity : Entity<TId>
    where TId : notnull;

public record GetPagedQuery<TEntity, TId>(int Page = 1, int PageSize = 20, string? SortBy = null, bool Descending = false)
    : IRequest<Result<PagedResult<TEntity>>>
    where TEntity : Entity<TId>
    where TId : notnull;

public record PagedResult<TEntity>(IReadOnlyList<TEntity> Items, int TotalCount, int Page, int PageSize);