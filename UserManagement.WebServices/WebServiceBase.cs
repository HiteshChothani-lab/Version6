using Newtonsoft.Json;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.WebServices
{
    public class WebServiceBase
    {
        protected HttpClient client;

        protected string BaseAddress { get; set; }

        private readonly Policy _policy;
        protected WebServiceBase(Action<HttpClient> httpClientModifier = null)
        {
//#if DEBUG
//            BaseAddress = "https://tuco-app.herokuapp.com/api/windows/";
//#else
//            BaseAddress = "http://kaushikmugdha.com/hamed/storelogistic/api/windows/";
//#endif
            BaseAddress = "https://tuco-app.herokuapp.com/api/windows/";

            client = new HttpClient(new HttpClientHandler() { UseProxy = false, MaxRequestContentBufferSize = Int32.MaxValue, AutomaticDecompression = DecompressionMethods.None | DecompressionMethods.Deflate | DecompressionMethods.GZip });

            httpClientModifier?.Invoke(this.client);

            _policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                                   TimeSpan.FromMilliseconds((200 * retryAttempt)),
                    (exception, timeSpan, context) =>
                    {
                        Debug.WriteLine(exception.ToString());
                    }
                );

        }

        internal enum RequestType
        {
            Delete,
            Get,
            Post,
            Put
        }

        protected Task<Tuple<string, T, HttpStatusCode, int>> GetAsync<T>(string requestUri, string accessToken = "")
        {
            return SendWithRetryAsync<T>(RequestType.Get, requestUri, null, null, accessToken);
        }

        protected Task<Tuple<string, T, HttpStatusCode, int>> PostAsync<T, K>(string requestUri, K obj, string accessToken = "")
        {
            var jsonRequest = !obj.Equals(default(K)) ? JsonConvert.SerializeObject(obj) : null;
            return SendWithRetryAsync<T>(RequestType.Post, requestUri, jsonRequest, null, accessToken);
        }

        protected Task<Tuple<string, T, HttpStatusCode, int>> PostStreamAsync<T, K>(string requestUri, K obj, byte[] stream, string accessToken = "")
        {
            var jsonRequest = !obj.Equals(default(K)) ? JsonConvert.SerializeObject(obj) : null;
            return SendWithRetryAsync<T>(RequestType.Post, requestUri, jsonRequest, stream, accessToken);
        }

        protected Task<Tuple<string, T, HttpStatusCode, int>> DeleteAsync<T>(string requestUri, string accessToken = "")
        {
            return SendWithRetryAsync<T>(RequestType.Delete, requestUri, null, null, accessToken);
        }

        protected Task<Tuple<string, T, HttpStatusCode, int>> PutAsync<T, K>(string requestUri, K obj, string accessToken = "") // where object
        {
            var jsonRequest = !obj.Equals(default(K)) ? JsonConvert.SerializeObject(obj) : null;
            return SendWithRetryAsync<T>(RequestType.Put, requestUri, jsonRequest, null, accessToken);
        }

        async Task<Tuple<string, T, HttpStatusCode, int>> SendWithRetryAsync<T>(RequestType requestType, string requestUri, string jsonRequest = null, byte[] stream = null, string accessToken = "")
        {
            Tuple<string, T, HttpStatusCode, int> result = new Tuple<string, T, HttpStatusCode, int>("", default(T), HttpStatusCode.BadRequest, 99);

            try
            {
                result = await _policy.ExecuteAsync(() =>
                {
                    return SendAsync<T>(requestType, requestUri, jsonRequest, stream, accessToken);
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        async Task<Tuple<string, T, HttpStatusCode, int>> SendAsync<T>(RequestType requestType, string requestUri, string jsonRequest = null, byte[] stream = null, string accessToken = "")
        {
            T result = default(T);

            HttpContent content = null;


            if (stream != null)
            {
                var multipartFormData = new MultipartFormDataContent();

                var imageContent = new ByteArrayContent(stream, 0, stream.Length);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                multipartFormData.Add(imageContent, "image", "hello1.jpg");
                multipartFormData.Add(new StringContent(jsonRequest, Encoding.UTF8, "application/json"), "jsondata");

                content = multipartFormData;
            }
            else
            {
                if (jsonRequest != null)
                    content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            }

            if (client.BaseAddress == null)
                client.BaseAddress = new Uri(BaseAddress);

            //if (!string.IsNullOrEmpty(accessToken))
            //{
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //}

            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Authorization", accessToken);
            }

            Task<HttpResponseMessage> httpTask;

            switch (requestType)
            {
                case RequestType.Get:
                    httpTask = client.GetAsync(requestUri);
                    break;
                case RequestType.Post:
                    httpTask = client.PostAsync(requestUri, content);
                    break;
                case RequestType.Delete:
                    httpTask = client.DeleteAsync(requestUri);
                    break;
                case RequestType.Put:
                    httpTask = client.PutAsync(requestUri, content);
                    break;
                default:
                    throw new ArgumentNullException("Not a valid request type");
            }

            var response = await httpTask;

            string json = string.Empty;

            if (response != null)
                json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!string.IsNullOrEmpty(json))
                result = JsonConvert.DeserializeObject<T>(json);

            var httpStatusCode = response != null ? response.StatusCode : HttpStatusCode.BadRequest;
            int genericStatusCode = -99;

            if (httpStatusCode == System.Net.HttpStatusCode.OK)
            {
                genericStatusCode = 1;
            }
            else if (httpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                genericStatusCode = -97;
            }
            else if (httpStatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                genericStatusCode = -98;
            }


            return new Tuple<string, T, HttpStatusCode, int>(json, result, response != null ? response.StatusCode : HttpStatusCode.BadRequest, genericStatusCode);
        }
    }
}
