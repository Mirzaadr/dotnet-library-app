using LibraryApp.Api.Models;
using LibraryApp.Domain.Users;
using Mapster;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.FullName)
            .Map(dest => dest.Status, src => src.Status.Name);
        // .Map(dest => dest, src => src);

        config.NewConfig<User, UserDetailResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.FullName)
            .Map(dest => dest.Role, src => src.Role.Name)
            .Map(dest => dest.Status, src => src.Status.Name);
    }
}