namespace Library.Web
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Text;
    using System.IO;
    using Library;
    using log4net;
    using Newtonsoft.Json;

    /// <summary>
    ///     The api caller.
    /// </summary>
    public class ApiCaller
    {
        /// <summary>
        /// The http client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// The prefix url.
        /// </summary>
        private readonly string prefixUrl;

        /// <summary>
        /// The value formatter.
        /// </summary>
        private readonly ValueFormatter valueFormatter;

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        protected static readonly ILog log = LogManager.GetLogger(typeof(ApiCaller));

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCaller"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http Client.
        /// </param>
        /// <param name="prefixUrl">
        /// The prefix Url.
        /// </param>
        public ApiCaller(HttpClient httpClient, string prefixUrl)
        {
            this.httpClient = httpClient;
            this.prefixUrl = prefixUrl;
            this.valueFormatter = new ValueFormatter();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="collectorName">
        /// The collector name.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of entity.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{TEntity}"/>.
        /// </returns>
        public IEnumerable<TEntity> GetAll<TEntity>(IDictionary<string, object> parameters = null)
            where TEntity : class
        {
            var queryString = this.valueFormatter.Format(parameters, "&");
            var requestUrl = this.prefixUrl;
            if (!string.IsNullOrEmpty(queryString))
            {
                requestUrl += "?" + queryString;
            }

            var responseMessage = this.httpClient.GetAsync(requestUrl).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;

                string mediaType = responseContent.Headers.ContentType.MediaType;

                if (mediaType.ToLower() == "application/bson") { return BsonHelper.Deserialize<IEnumerable<TEntity>>(readStream); }

                if (mediaType.ToLower() == "application/json") { return JsonHelper.DeserializeObject<IEnumerable<TEntity>>(readStream); }

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }

            throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
        }
        public Stream GetStream(IDictionary<string, object> parameters)
        {
            var queryString = this.valueFormatter.Format(parameters, "&");
            var requestUrl = this.prefixUrl;
            if (!string.IsNullOrEmpty(queryString))
            {
                requestUrl += "?" + queryString;
            }

            var responseMessage = this.httpClient.GetAsync(requestUrl).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;

                string mediaType = responseContent.Headers.ContentType.MediaType;

                return readStream;

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            this.LogErrorResponse(responseMessage);
            throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
        }
        public object GetJson(IDictionary<string, object> parameters)
        {
            var stream = this.GetStream(parameters);
            var streamReader = new StreamReader(stream);
            var content = streamReader.ReadToEnd();
            return content;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="collectorName">
        /// The collector name.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="TEntity">
        /// Type of entity.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{TEntity}"/>.
        /// </returns>
        public TEntity GetOne<TEntity>(object id)
            where TEntity : class
        {
            var type = id.GetType();
            var requestUrl = this.prefixUrl;
            if (type == typeof(int))
            {
                requestUrl += "(" + id + ")";
            }
            else // if (type == typeof(string))
            {
                requestUrl += "('" + id + "')";
            }

            var responseMessage = this.httpClient.GetAsync(requestUrl).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;

                string mediaType = responseContent.Headers.ContentType.MediaType;

                if (mediaType.ToLower() == "application/bson") { return BsonHelper.Deserialize<TEntity>(readStream); }

                if (mediaType.ToLower() == "application/json") { return JsonHelper.DeserializeObject<TEntity>(readStream); }

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else return null;
        }

        public TOutEntity PostGetObject<TOutEntity>(object data, IDictionary<string, object> parameters = null)
        {
            var queryString = this.valueFormatter.Format(parameters, "&");
            var requestUrl = this.prefixUrl;
            if (!string.IsNullOrEmpty(queryString))
            {
                requestUrl += "?" + queryString;
            }

            var content = new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());

            var responseMessage = this.httpClient.PostAsync(requestUrl, content).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK || responseMessage.StatusCode == HttpStatusCode.Created)
            {
                var responseString = responseMessage.Content.ReadAsStringAsync().Result;

                var responseContent = responseMessage.Content;
                string mediaType = responseContent.Headers.ContentType.MediaType;
                if (mediaType.ToLower() == "text/plain")
                {
                    var value = Convert.ChangeType(responseString, typeof(TOutEntity));
                    return value is TOutEntity ? (TOutEntity)value : default(TOutEntity);
                }
                else if (mediaType.ToLower() == "application/json")
                {
                    var readStream = responseContent.ReadAsStreamAsync().Result;
                    return JsonHelper.DeserializeObject<TOutEntity>(readStream);
                }

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }

            throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
        }
        public TOutEntity Post<TOutEntity, TInEntity>(TInEntity data)
            where TOutEntity : class
            where TInEntity : class
        {
            var requestUrl = this.prefixUrl;
            //var content = new ObjectContent<TInEntity>(data, new JsonMediaTypeFormatter());
            var content = this.GetContent(data);

            var responseMessage = this.httpClient.PostAsync(requestUrl, content).Result;
            if (responseMessage.StatusCode == HttpStatusCode.Created)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;

                string mediaType = responseContent.Headers.ContentType.MediaType;
                if (mediaType.ToLower() == "application/json") { return JsonHelper.DeserializeObject<TOutEntity>(readStream); }

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }

            throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
        }
        public TEntity Post<TEntity>(TEntity data)
            where TEntity : class
        {
            return this.Post<TEntity, TEntity>(data);
        }
        public void PostStream(Stream data)
        {
            var requestUrl = this.prefixUrl;
            var content = new StreamContent(data);

            var responseMessage = this.httpClient.PostAsync(requestUrl, content).Result;
            if (responseMessage.StatusCode == HttpStatusCode.Created)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;

                string mediaType = responseContent.Headers.ContentType.MediaType;

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }

            throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
        }

        public void Patch<TEntity>(object id, TEntity data)
            where TEntity : class
        {
            var requestUrl = this.prefixUrl;

            var type = id.GetType();
            if (type == typeof(int))
            {
                requestUrl += "(" + id + ")";
            }
            else // if (type == typeof(string))
            {
                requestUrl += "('" + id + "')";
            }

            //var content = new ObjectContent<TEntity>(data, new JsonMediaTypeFormatter());
            var content = this.GetContent(data);

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUrl)
            {
                Content = content
            };

            var responseMessage = httpClient.SendAsync(request).Result;
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }
            else if (responseMessage.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                throw new Exception("Api không hỗ trợ hàm Update");
            }
            else if (responseMessage.StatusCode == HttpStatusCode.NotImplemented)
            {
                throw new Exception("Api không hỗ trợ hàm Update");
            }
        }
        public TOutEntity Put<TOutEntity, TInEntity>(object id, TInEntity data)
            where TOutEntity : class
            where TInEntity : class
        {
            var requestUrl = this.prefixUrl;
            var type = id.GetType();
            if (type == typeof(int))
            {
                requestUrl += "(" + id + ")";
            }
            else // if (type == typeof(string))
            {
                requestUrl += "('" + id + "')";
            }

            //var content = new ObjectContent<TInEntity>(data, new JsonMediaTypeFormatter());
            var content = this.GetContent(data);

            var responseMessage = this.httpClient.PutAsync(requestUrl, content).Result;
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = responseMessage.Content;
                var readStream = responseContent.ReadAsStreamAsync().Result;

                string mediaType = responseContent.Headers.ContentType.MediaType;
                if (mediaType.ToLower() == "application/json") { return JsonHelper.DeserializeObject<TOutEntity>(readStream); }

                throw new NotSupportedException(string.Format("MediaType {0} is not support", mediaType));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return data as TOutEntity;
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }

            throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
        }
        public void Put<TEntity>(TEntity data)
            where TEntity : class
        {
            var requestUrl = this.prefixUrl;
            var content = new ObjectContent<TEntity>(data, new JsonMediaTypeFormatter());

            var method = new HttpMethod("PUT");
            var request = new HttpRequestMessage(method, requestUrl)
            {
                Content = content
            };

            var responseMessage = httpClient.SendAsync(request).Result;
        }

        public void Delete(object id)
        {
            var requestUrl = this.prefixUrl;
            var type = id.GetType();
            //if (type == typeof(int))
            //{
            requestUrl += "(" + id + ")";
            //}
            //else if (type == typeof(string))
            //{
            //    requestUrl += "('" + id + "')";
            //}

            var method = new HttpMethod("DELETE");
            var request = new HttpRequestMessage(method, requestUrl);

            var responseMessage = this.httpClient.SendAsync(request).Result;
            if (responseMessage.StatusCode != HttpStatusCode.NoContent)
            {
                this.LogErrorResponse(responseMessage);
                throw new InvalidOperationException(string.Format("Request to {0} return invalid status code {1}", requestUrl, responseMessage.StatusCode));
            }
            else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }
            else if (responseMessage.StatusCode == HttpStatusCode.InternalServerError || responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = GetErrorMessage(responseMessage);
                throw new Exception(message);
            }
        }
        #endregion

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// The log error response.
        /// </summary>
        /// <param name="responseMessage">
        /// The response message.
        /// </param>
        private void LogErrorResponse(HttpResponseMessage responseMessage)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("StatusCode: {0}, ReasonPhrase: '{1}'", responseMessage.StatusCode, responseMessage.ReasonPhrase);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Headers");
            stringBuilder.AppendLine("{");
            foreach (var httpResponseHeader in responseMessage.Headers)
            {
                stringBuilder.AppendFormat(
                    "{0}: {1}", httpResponseHeader.Key, string.Join(",", httpResponseHeader.Value));
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine("}");

            if (responseMessage.Content != null && responseMessage.Content.Headers.ContentLength < 1024 * 100)
            {
                try
                {
                    stringBuilder.AppendLine("Content:");
                    var content = responseMessage.Content.ReadAsStringAsync().Result;
                    stringBuilder.AppendLine(content);
                }
                catch (Exception ex)
                {
                    log.Warn("Cannot read content from error response message", ex);
                }
            }

            string error = stringBuilder.ToString();
            log.Error(error);
        }

        private string GetErrorMessage(HttpResponseMessage responseMessage)
        {
            var response = responseMessage.Content.ReadAsStringAsync().Result;
            if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                if (response.StartsWith("{\"Message\":\"")) response = response.Substring(12, response.Length - 14);
            }
            else if (response.Contains("BusinessException"))
            {
                if (response.Contains("\"innererror\":{\r\n      \"message\":\""))
                {
                    response = response.Split(new string[] { "\"innererror\":{\r\n      \"message\":\"" }, System.StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                    response = response.Split(new string[] { "\",\"type\":\"System.Exception\"" }, System.StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                }
            }
            else if (response.Contains("System.NotSupportedException"))
            {
                response = "Phương thức không được hỗ trợ - Vui lòng liên hệ kỹ thuật";
                this.LogErrorResponse(responseMessage);
            }
            else
            {
                response = "Lỗi xử lý nghiệp vụ - Vui lòng liên hệ kỹ thuật";
                this.LogErrorResponse(responseMessage);
            }

            return response;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.httpClient.Dispose();
                this.disposed = true;
            }
        }

        public HttpContent GetContent<TEntity>(TEntity entity)
        {
            var postData = new List<KeyValuePair<string, string>>();

            var type = typeof(TEntity);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                if (value == null) continue;
                postData.Add(new KeyValuePair<string, string>(property.Name, value.ToString()));
            }

            var jsonString = JsonConvert.SerializeObject(postData);
            //HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpContent content = new FormUrlEncodedContent(postData);

            return content;
        }
    }
}