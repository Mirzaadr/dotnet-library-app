// Application/CrudEngine/Handlers/CreateCommandHandler.cs
using LibraryApp.Domain.Common.Models;
using LibraryApp.Application.Abstractions;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.CrudEngine.Handlers;

public class CreateCommandHandler<TEntity, TId> : IRequestHandler<CreateCommand<TEntity, TId>, Result<TEntity>>
    where TEntity : Entity<TId>
    where TId : notnull
{
    private readonly IAppDBContext _db;

    public CreateCommandHandler(IAppDBContext db) => _db = db;

    public async Task<Result<TEntity>> Handle(CreateCommand<TEntity, TId> request, CancellationToken ct)
    {
        // request.Entity is expected to arrive already fully-formed via your domain
        // factory (e.g. Book.Create(...)), including its own generated Id.
        _db.Set<TEntity>().Add(request.Entity);
        await _db.SaveChangesAsync(ct);
        return Result<TEntity>.Success(request.Entity);
    }
}