using System.Text.Json;

namespace Utilities
{
    public class CommonHelper
    {
        public static T DeepCopy<T>(T source)
        {
            if (source == null)
            {
                return default(T);
            }

             var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            var json = JsonSerializer.Serialize(source, options);
            return JsonSerializer.Deserialize<T>(json, options);
        }

    }
}