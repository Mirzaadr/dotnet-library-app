using LibraryApp.Domain.Common.Models;
using MediatR;

namespace LibraryApp.Application.CrudEngine;

public record CreateCommand<TEntity, TId>(TEntity Entity) : IRequest<Result<TEntity>>
    where TEntity : Entity<TId>
    where TId : notnull;

public record UpdateCommand<TEntity, TId>(TId Id, TEntity Entity) : IRequest<Result<TEntity>>
    where TEntity : Entity<TId>
    where TId : notnull;

public record DeleteCommand<TEntity, TId>(TId Id) : IRequest<Result<bool>>
    where TEntity : Entity<TId>
    where TId : notnull;