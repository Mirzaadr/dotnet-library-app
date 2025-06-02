using LibraryApp.Api.Models;
using LibraryApp.Domain.Books;
using LibraryApp.Domain.BorrowRecords;
using Mapster;

public class BorrowRecordMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BorrowRecord, BorrowRecordResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.BookId, src => src.BookId.Value)
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Status, src => src.Status.Name);
        // .Map(dest => dest, src => src);
    }
}