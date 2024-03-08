using System.Text.Json;
using Store.API.RequestHelpers;

namespace Store.API.Extentions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        response.Headers.Append("Pagination", JsonSerializer.Serialize(metaData, options));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}