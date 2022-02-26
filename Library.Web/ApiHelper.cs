using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Library.Web;
using System.Net.Http.Formatting;

namespace Library.Web
{
    public class ApiHelper
    {
        private string UserName;
        private string Password;
        public string Token;

        public ApiHelper()
        {
            this.Token = this.Password = this.UserName = string.Empty;
        }

        public ApiHelper(string userName = "", string password = "")
        {
            this.UserName = userName;
            this.Password = userName;
        }

        public ApiHelper(string token = "")
        {
            this.Token = token;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="contextPath">
        /// The context Path.
        /// </param>
        /// <returns>
        /// The <see cref="IApiCaller"/>.
        /// </returns>
        public IEnumerable<TEntity> GetAll<TEntity>(string url, IDictionary<string, object> parameters = null) where TEntity : class
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson", 1));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

                var apiCaller = new ApiCaller(httpClient, url);

                var data = apiCaller.GetAll<TEntity>(parameters);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return data;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw new Exception(string.Format("Cannot create ApiCaller. Api: {0}", url), ex);
            }
        }
        public object Get(string url, IDictionary<string, object> parameters = null)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson", 1));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

                var apiCaller = new ApiCaller(httpClient, url);

                var data = apiCaller.GetJson(parameters);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return data;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw new Exception(string.Format("Cannot create ApiCaller. Api: {0}", url), ex);
            }
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="contextPath">
        /// The context Path.
        /// </param>
        /// <returns>
        /// The <see cref="IApiCaller"/>.
        /// </returns>
        public TEntity GetOne<TEntity>(string url, object id) where TEntity : class
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson", 1));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

                var apiCaller = new ApiCaller(httpClient, url);

                var data = apiCaller.GetOne<TEntity>(id);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return data;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw new ApplicationException(string.Format("Cannot create ApiCaller. Api: {0}", url), ex);
            }
        }


        public TOutEntity PostGetObject<TOutEntity>(string url, object data, IDictionary<string, object> parameters = null)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);
                    httpClient.DefaultRequestHeaders.Add("Token", this.Token);
                }

                var apiCaller = new ApiCaller(httpClient, url);

                var outputData = apiCaller.PostGetObject<TOutEntity>(data, parameters);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return outputData;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw ex;
            }
        }
        public TEntity Insert<TEntity>(string url, TEntity inputData) where TEntity : class
        {
            return this.Post<TEntity, TEntity>(url, inputData);
        }
        public TOutEntity Post<TOutEntity, TInEntity>(string url, TInEntity inputData) where TOutEntity : class where TInEntity : class
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

                var apiCaller = new ApiCaller(httpClient, url);

                var outputData = apiCaller.Post<TOutEntity, TInEntity>(inputData);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return outputData;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw ex;
            }
        }
        public void PostStream(string url, Stream inputData)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);
                var apiCaller = new ApiCaller(httpClient, url);
                apiCaller.PostStream(inputData);
                apiCaller.Dispose();
                httpClientHandler.Dispose();
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw new ApplicationException(string.Format("Cannot create ApiCaller. UrlBase: {0}", url), ex);
            }
        }

        public TOutEntity Put<TOutEntity, TInEntity>(string url, object id, TInEntity inputData) where TOutEntity : class where TInEntity : class
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

                var apiCaller = new ApiCaller(httpClient, url);

                var outputData = apiCaller.Put<TOutEntity, TInEntity>(id, inputData);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return outputData;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw ex;
            }
        }
        public TEntity Update<TEntity>(string url, object id, TEntity data)
            where TEntity : class
        {
            return this.Put<TEntity, TEntity>(url, id, data);
        }
        public void Patch<TEntity>(string url, object id, TEntity data)
            where TEntity : class
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            if (!string.IsNullOrEmpty(this.UserName))
                httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

            if (!string.IsNullOrEmpty(this.Token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

            var apiCaller = new ApiCaller(httpClient, url);

            apiCaller.Patch<TEntity>(id, data);

            apiCaller.Dispose();
            httpClientHandler.Dispose();
        }

        public void Delete(string url, object id)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                if (!string.IsNullOrEmpty(this.UserName))
                    httpClientHandler.Credentials = new NetworkCredential(this.UserName, this.Password);

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                if (!string.IsNullOrEmpty(this.Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", this.Token);

                var apiCaller = new ApiCaller(httpClient, url);

                apiCaller.Delete(id);

                apiCaller.Dispose();
                httpClientHandler.Dispose();
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    throw;
                }

                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException();
                }

                throw new ApplicationException(string.Format("Cannot create ApiCaller. UrlBase: {0}", url), ex);
            }
        }

        private List<KeyValuePair<string, string>> GetPostData<TEntity>(TEntity entity)
        {
            var postData = new List<KeyValuePair<string, string>>();

            var type = typeof(TEntity);
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public);
            foreach (var property in properties)
            {
                postData.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(entity).ToString()));
            }

            return postData;
        }
    }
}
