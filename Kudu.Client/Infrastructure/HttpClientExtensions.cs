﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kudu.Client.Infrastructure
{
    public static class HttpClientExtensions
    {
        public static T GetJson<T>(this HttpClient client, string url)
        {
            var response = client.GetAsync(url);
            var content = response.Result.EnsureSuccessful().Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(content);
        }

        public static Task<T> GetJsonAsync<T>(this HttpClient client, string url)
        {
            return client.GetAsync(url).Then(result =>
            {
                return result.EnsureSuccessful().Content.ReadAsStringAsync().Then(content =>
                {
                    return JsonConvert.DeserializeObject<T>(content);
                });
            });
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client)
        {
            return client.PostAsync(String.Empty, new StringContent(String.Empty)).Then(result =>
            {
                return result.EnsureSuccessful();
            });
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri)
        {
            return client.PostAsync(requestUri, new StringContent(String.Empty)).Then(result =>
            {
                return result.EnsureSuccessful();
            });
        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri)
        {
            return client.PutAsync(requestUri, new StringContent(String.Empty)).Then(result =>
            {
                return result.EnsureSuccessful();
            });
        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri, params KeyValuePair<string, string>[] items)
        {
            return client.PutAsync(requestUri, HttpClientHelper.CreateJsonContent(items)).Then(result =>
            {
                return result.EnsureSuccessful();
            });
        }

        public static Task<HttpResponseMessage> DeleteSafeAsync(this HttpClient client, string requestUri)
        {
            return client.DeleteAsync(requestUri).Then(result =>
            {
                return result.EnsureSuccessful();
            });
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, KeyValuePair<string, string> param)
        {
            return client.PostAsync(url, HttpClientHelper.CreateJsonContent(param)).Then(result =>
            {
                return result.EnsureSuccessful();
            });
        }
    }
}
