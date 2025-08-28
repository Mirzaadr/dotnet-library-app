using System.Text.Json;
using LibraryApp.Api.Common.Helper;

namespace LibraryApp.Api.Extensions;


public static class HttpExtensions
{
  public static void AddPaginationHeader(this HttpResponse response,
  int currentPage, int itemsPerPage, int totalItems, int totalPages)
  {

    var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
    var options = new JsonSerializerOptions()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, options));
    response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
  }
}