using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace AdventureGame.Persistence.Converters
{
    public class Vector2EnumerableConverter : JsonConverter<IEnumerable<Vector2>>
    {
        public override void WriteJson(JsonWriter writer, IEnumerable<Vector2> value, JsonSerializer serializer)
        {
            List<Vector2Data> vectors = new List<Vector2Data>();
            foreach (Vector2 originalVector in value)
            {
                vectors.Add(originalVector);
            }
            
            serializer.Serialize(writer, vectors);
        }

        public override IEnumerable<Vector2> ReadJson(JsonReader reader, Type objectType, IEnumerable<Vector2> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            IEnumerable<Vector2Data> vectors = serializer.Deserialize<IEnumerable<Vector2Data>>(reader);

            List<Vector2> result = new List<Vector2>();
            foreach (Vector2Data convertedVector in vectors)
            {
                result.Add(convertedVector);
            }
            
            return result;
        }
    }

    [Serializable]
    public class Vector2Data
    {
        public float x;
        public float y;

        public Vector2Data(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        public static implicit operator Vector3(Vector2Data data) => new Vector3(data.x, data.y);
        public static implicit operator Vector2Data(Vector3 vector) => new Vector2Data(vector);
        public static implicit operator Vector2(Vector2Data data) => new Vector2(data.x, data.y);
        public static implicit operator Vector2Data(Vector2 vector) => new Vector2Data(vector);
    }
}