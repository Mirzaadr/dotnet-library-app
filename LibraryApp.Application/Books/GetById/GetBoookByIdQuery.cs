using LibraryApp.Domain.Common.Models;
using MediatR;

public class GetBookByIdQuery : IRequest<Result<GetBookByIdResponse>>
{
    public Guid Id { get; }

    public GetBookByIdQuery(Guid id)
    {
      Id = id;
    }
}