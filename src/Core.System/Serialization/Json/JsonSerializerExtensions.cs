using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
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

			return JsonSerializer.Deserialize<T>(utf8Json, JsonSerializerOptions);
		}

		private static readonly ConcurrentDictionary<Type, MethodInfo> Utf8JsonToObjectCache =
			new ConcurrentDictionary<Type, MethodInfo>();

		public static object Utf8JsonToObject(this byte[] utf8Json, Type targetTypeName)
		{
			var genericMethod = Utf8JsonToObjectCache.GetOrAdd(targetTypeName,
				(k) => typeof(JsonSerializer)
					.GetMethod(nameof(JsonSerializer.Deserialize),
						new Type[] {typeof(ReadOnlySpan<byte>), typeof(JsonSerializerOptions)})
					.MakeGenericMethod(targetTypeName));
			return genericMethod.Invoke(null, new object[] {utf8Json});
		}
	}
}
