using Microsoft.Extensions.DependencyInjection;
using MediatR;
using LibraryApp.Application.CrudEngine.Handlers;
using LibraryApp.Domain.Books;
using LibraryApp.Application.CrudEngine;
using LibraryApp.Domain.Common.Models;

namespace LibraryApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<
            IRequestHandler<GetPagedQuery<Book, BookId>, Result<PagedResult<Book>>>, 
            GetPagedQueryHandler<Book, BookId>
        >(); 


        return services;
    }
}
