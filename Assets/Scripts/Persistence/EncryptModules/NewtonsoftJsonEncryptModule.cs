using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventureGame.Persistence.EncryptModules
{
    public class NewtonsoftJsonEncryptModule : IEncryptModule
    {
        private static JsonSerializerSettings Settings => new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            ContractResolver = new WritablePropertiesOnlyResolver()
        };

        public string Encrypt<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Settings);
        }

        public T Decrypt<T>(string data)
        {
            using (StringReader sw = new StringReader(data))
            {
                using (CustomReader jsonReader = new CustomReader(sw))
                {
                    return JsonSerializer.CreateDefault(Settings).Deserialize<T>(jsonReader);
                }
            }
        }
        
        /// <summary>
        /// Used to serialize only writable fields
        /// </summary>
        private class WritablePropertiesOnlyResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
                return props.Where(p => p.Writable).ToList();
            }
        }

        /// <summary>
        /// Used to give preference to deserialize numeric objects as Int instead Long
        /// </summary>
        /// <remarks>
        /// Source: https://stackoverflow.com/questions/8297541/how-do-i-change-the-default-type-for-numeric-deserialization
        /// </remarks>
        private class CustomReader : JsonTextReader
        {
            public CustomReader(TextReader reader) : base(reader) { }

            public override bool Read()
            {
                bool ret = base.Read();

                if (TokenType == JsonToken.Integer && ValueType == typeof(long) && Value is long)
                {
                    var value = (long)Value;

                    if (value <= int.MaxValue && value >= int.MinValue)
                    {
                        int newValue = checked((int)value); // checked just in case
                        SetToken(TokenType, newValue, false);
                    }
                }

                return ret;
            }
        }
    }
}