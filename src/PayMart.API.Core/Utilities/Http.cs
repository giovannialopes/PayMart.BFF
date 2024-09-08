using Newtonsoft.Json;
using PayMart.Infrastructure.Core.Services;
namespace PayMart.API.Core.Utilities;

public class Http
{
    public static async Task<(TResponse? Response, string? ErrorMessage)> HandleResponse<TResponse>(HttpResponseMessage httpResponse)
    {
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.IsSuccessStatusCode && responseContent.Contains("{"))
        {
            var response = JsonConvert.DeserializeObject<TResponse>(responseContent);
            return (response, null);
        }

        return (default, responseContent);
    }


}
