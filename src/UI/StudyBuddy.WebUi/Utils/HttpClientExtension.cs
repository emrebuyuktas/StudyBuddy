﻿using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using StudyBuddy.WebUi.CustomException;
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
            Console.WriteLine(responseData);
            var res = JsonSerializer.Deserialize<Response<TResult>>(responseData,jsonSerilizerOptions);
          

            return res;
        }
    

    public static async Task<Response<TValue>> PostGetBaseResponseAsync<TValue>(this HttpClient Client, String Url, TValue Value)
        {
            var httpRes = await Client.PostAsJsonAsync(Url, Value);

            
            
                return await httpRes.Content.ReadFromJsonAsync<Response<TValue>>();

            
          
            
        }


        public static async Task<Response<T>> GetServiceResponseAsync<T>(this HttpClient Client, String Url,bool throwWhenNotSuccess=false)
        {
            
            
            var httpRes = await Client.GetFromJsonAsync<Response<T>>(Url);
            if (httpRes is null && throwWhenNotSuccess)
            {
                throw new ApiException(httpRes?.Error.Errors.First());
            }

            return httpRes;
        }
        
        
    }
}
