using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.System.Serialization.Json
{
	public static class JsonSerializerExtensions
	{
		private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
		{
				
		};

		public static string ToJson<T>(this T src)
		{
			return JsonSerializer.Serialize(src, JsonSerializerOptions);
		}
		public static byte[] ToUtf8Json<T>(this T src)
		{
			return JsonSerializer.SerializeToUtf8Bytes(src, JsonSerializerOptions);
		}

		public static T JsonToObject<T>(this string src)
		{
			return JsonSerializer.Deserialize<T>(src, JsonSerializerOptions);
		}
		public static T Utf8JsonToObject<T>(this byte[] utf8Json)
		{
			return JsonSerializer.Deserialize<T>(utf8Json,JsonSerializerOptions);
		}
	}
}
