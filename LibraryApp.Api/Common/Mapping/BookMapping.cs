using LibraryApp.Api.Models;
using LibraryApp.Domain.Books;
using Mapster;

public class BookMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // config.NewConfig<(CreateBookRequest Request, string HostId), CreateBookCommand>()
        //     .Map(dest => dest.HostId, src => src.HostId)
        //     .Map(dest => dest, src => src.Request);

        config.NewConfig<Book, BookResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Author, src => src.Author)
            .Map(dest => dest.Genre, src => src.Genre);
        // .Map(dest => dest, src => src);

        config.NewConfig<Book, BookDetailResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        // config.NewConfig<Author, AuthorResponse>()
        //     .Map(dest => dest.Id, src => src.Id.Value);

        // config.NewConfig<Category, CategoryResponse>()
        //     .Map(dest => dest.Id, src => src.Id.Value);

        // config.NewConfig<Publisher, PublisherResponse>()
        //     .Map(dest => dest.Id, src => src.Id.Value);
    }
}