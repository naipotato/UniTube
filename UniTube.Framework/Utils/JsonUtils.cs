using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Foundation;

namespace UniTube.Framework.Utils
{
    public static class JsonUtils
    {
        public static IAsyncOperation<T> ToObjectAsync<T>(string value)
            => Task.Run(() => JsonConvert.DeserializeObject<T>(value)).AsAsyncOperation();

        public static IAsyncOperation<string> StringifyAsync(object value)
            => Task.Run(() => JsonConvert.SerializeObject(value)).AsAsyncOperation();
    }
}
