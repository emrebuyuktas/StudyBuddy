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
        public async static Task<Response<TResult>> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient Client,
            String Url, TValue Value, bool ThrowSuccessException = false)
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
    

    public async static Task<Response<TValue>> PostGetBaseResponseAsync<TValue>(this HttpClient Client, String Url, TValue Value, bool ThrowSuccessException = false)
        {
            var httpRes = await Client.PostAsJsonAsync(Url, Value);

            if (httpRes.IsSuccessStatusCode)
            {
                var res = await httpRes.Content.ReadFromJsonAsync<Response<TValue>>();

            }
            return null;
        }


        public async static Task<Response<T>> GetServiceResponseAsync<T>(this HttpClient Client, String Url, bool ThrowSuccessException = false)
        {
            var httpRes = await Client.GetFromJsonAsync<Response<T>>(Url);
            return httpRes;
        }
        
    }
}
