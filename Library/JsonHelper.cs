namespace Library
{
    using System.IO;

    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// The json helper.
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// The deserialize object.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <typeparam name="TObject">
        /// Type of object
        /// </typeparam>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static TObject DeserializeObject<TObject>(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            var content = streamReader.ReadToEnd();
            if (content.Contains("http://") && content.Contains("odata"))
            {
                if (content.Contains("\"value\":[\r\n    \r\n  ]\r\n}")) return default(TObject); //Activator.CreateInstance<TObject>();
                if (content.Contains("\"results\": ["))
                {
                    content = content.Split(new string[] { "\"results\": [" }, System.StringSplitOptions.RemoveEmptyEntries)[1];
                    content = content.Substring(0, content.Length - 1).Trim(); 
                    content = content.Replace("\"__metadata\":", string.Empty).Replace("{\r\n {\r\n","{\r\n").Replace("}, \"ID\"", ", \"ID\"");
                    content = "[" + content.Substring(0, content.Length - 4) + "]";
                }
                else if (content.Contains("@Element"))
                {
                    var index = content.IndexOf("@Element");
                    content = content.Substring(index + 10);
                    content = "{" + content.Trim();
                }
                else if (content.Contains("\"ID\":"))
                {
                    var index = content.IndexOf("\"ID\":");
                    content = content.Substring(index);
                    content = "{" + content.Substring(0, content.Length - 1).Trim();
                    if (content.EndsWith("]")) content = "[" + content;
                }
                
                //return JsonConvert.DeserializeObject<TObject>(content);
            }
            //else return JsonConvert.DeserializeObject<TObject>(content);
            try
            {
                var obj = JsonConvert.DeserializeObject<TObject>(content);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Can't convert json to object", new Exception(content + ", " + ex.Message));
            }
        }

        public static TObject DeserializeObject<TObject>(string jsonString)
        {
            return JsonConvert.DeserializeObject<TObject>(jsonString);
        }

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}