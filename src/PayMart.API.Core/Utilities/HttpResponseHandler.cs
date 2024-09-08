using Newtonsoft.Json;

namespace PayMart.API.Core.Utilities;

public class HttpResponseHandler
{


    public static async Task<T?> HandleResponseAsync<T>(HttpResponseMessage httpResponse) where T : class, new()
    {
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            var response = JsonConvert.DeserializeObject<T>(responseContent);
            return response!;
        }

        return default;
    }

    public static async Task<T?> PostAsync<T>(HttpClient httpClient, string url, object request) where T : class, new()
    {
        var httpResponse = await httpClient.PostAsJsonAsync(url, request);
        return await HandleResponseAsync<T>(httpResponse);
    }

    public static async Task<T?> PutAsync<T>(HttpClient httpClient, string url, object request) where T : class, new()
    {
        var httpResponse = await httpClient.PutAsJsonAsync(url, request);
        return await HandleResponseAsync<T>(httpResponse);
    }

    public static async Task<string?> DeleteAsync(HttpClient httpClient, string url)
    {
        var httpResponse = await httpClient.DeleteAsync(url);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        if (responseContent != "")
            return "Ok";

        return default;
    }
}
