namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    /// <summary>
    ///     The bson helper.
    /// </summary>
    public class BsonHelper
    {
        /// <summary>
        /// The default date time kind.
        /// </summary>
        private const DateTimeKind DefaultDateTimeKind = DateTimeKind.Unspecified;

        /// <summary>
        /// The json serializer settings.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();

        #region Public Methods and Operators

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="bsonInput">
        /// The bson input.
        /// </param>
        /// <typeparam name="TObject">
        /// Type of object.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TObject"/>.
        /// </returns>
        public static TObject Deserialize<TObject>(string bsonInput)
        {
            var buffer = Encoding.UTF8.GetBytes(bsonInput);
            using (var stringStream = new MemoryStream(buffer))
            {
                stringStream.Seek(0, SeekOrigin.Begin);
                return (TObject)Deserialize(stringStream, typeof(TObject));
            }
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="inputObj">
        /// The input object.
        /// </param>
        /// <typeparam name="TObject">
        /// Type of object.
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Serialize<TObject>(TObject inputObj)
        {
            var jsonSerializer = new JsonSerializer();
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var bson = new BsonWriter(ms))
                {
                    bson.DateTimeKindHandling = DefaultDateTimeKind;
                    
                    jsonSerializer.Serialize(bson, inputObj);
                    bytes = ms.ToArray();
                }
            }

            return Encoding.UTF8.GetString(bytes);
        }

        #endregion

        /// <summary>
        /// The deserialize many.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <typeparam name="TObject">
        /// Type of object.
        /// </typeparam>
        /// <returns>
        /// The <see cref="TObject"/>.
        /// </returns>
        public static TObject Deserialize<TObject>(Stream stream)
        {
            return (TObject)Deserialize(stream, typeof(TObject));
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object Deserialize(Stream stream, Type type)
        {
            using (var reader = new BsonReader(stream))
            {
                reader.DateTimeKindHandling = DefaultDateTimeKind;

                if (type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
                {
                    reader.ReadRootValueAsArray = true;
                }

                var jsonSerializer = JsonSerializer.Create(JsonSerializerSettings);
                return jsonSerializer.Deserialize(reader, type);
            }
        }
    }
}