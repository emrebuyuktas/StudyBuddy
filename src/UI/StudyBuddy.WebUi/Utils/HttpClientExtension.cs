using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using StudyBuddy.WebUi.Models;
using StudyBuddy.WebUi.Wrappers;

namespace StudyBuddy.WebUi.Utils
{
    public static class HttpClientExtension
    {
        private static JsonSerializerOptions jsonSerilizerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            
        };
        public static async Task<Response<TResult>> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient Client,
            String Url, TValue Value)
        {
            var httpRes = await Client.PostAsJsonAsync(Url, Value);
            var responseData = await httpRes.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<Response<TResult>>(responseData,jsonSerilizerOptions);
            if (res.StatusCode ==201)
            {

                return res;

            }

            return null;
        }
    

    public static async Task<Response<TValue>> PostGetBaseResponseAsync<TValue>(this HttpClient Client, String Url, TValue Value)
        {
            var httpRes = await Client.PostAsJsonAsync(Url, Value);

            
            
                return await httpRes.Content.ReadFromJsonAsync<Response<TValue>>();

            
          
            
        }


        public async static Task<Response<T>> GetServiceResponseAsync<T>(this HttpClient Client, String Url)
        {
            var httpRes = await Client.GetFromJsonAsync<Response<T>>(Url);
            return httpRes;
        }
        
    }
}
