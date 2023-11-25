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

            // 使用 JSON 序列化和反序列化進行深度複製
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