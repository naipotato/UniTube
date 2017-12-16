using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace UniTube.Framework.Utils
{
    public static class SettingsStorageUtils
    {
        private const string FileExtension = ".json";

        public static bool IsRoamingStorageAvailable(this ApplicationData appData) => appData.RoamingStorageQuota == 0;

        public static IAsyncAction SaveAsync<T>(this StorageFolder folder, string name, T content)
            => Task.Run(async () =>
            {
                var file = await folder.CreateFileAsync(GetFileName(name), CreationCollisionOption.ReplaceExisting);
                var fileContent = await JsonUtils.StringifyAsync(content);

                await FileIO.WriteTextAsync(file, fileContent);
            }).AsAsyncAction();

        public static IAsyncOperation<T> ReadAsync<T>(this StorageFolder folder, string name) => Task.Run(async () =>
        {
            if (!File.Exists(Path.Combine(folder.Path, GetFileName(name)))) return default(T);

            var file = await folder.GetFileAsync($"{name}.json");
            var fileContent = await FileIO.ReadTextAsync(file);

            return await JsonUtils.ToObjectAsync<T>(fileContent);
        }).AsAsyncOperation();

        public static IAsyncAction SaveAsync<T>(this ApplicationDataContainer settings, string key, T value)
            => Task.Run(async () => settings.Values[key] = await JsonUtils.StringifyAsync(value)).AsAsyncAction();

        public static IAsyncOperation<T> ReadAsync<T>(this ApplicationDataContainer settings, string key)
            => Task.Run(async () =>
            {
                if (settings.Values.TryGetValue(key, out var obj)) return await JsonUtils.ToObjectAsync<T>((string)obj);

                return default(T);
            }).AsAsyncOperation();

        public static IAsyncOperation<StorageFile> SaveFileAsync(this StorageFolder folder, byte[] content, string fileName, CreationCollisionOption options = CreationCollisionOption.ReplaceExisting)
            => Task.Run(async () =>
            {
                if (content == null)
                    throw new ArgumentNullException("content");

                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentException("File name is null or empty. Specify a valid file name", "fileName");

                var storageFile = await folder.CreateFileAsync(fileName, options);
                await FileIO.WriteBytesAsync(storageFile, content);
                return storageFile;
            }).AsAsyncOperation();

        public static IAsyncOperation<byte[]> ReadFileAsync(this StorageFolder folder, string fileName)
            => Task.Run(async () =>
            {
                var item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);

                if ((item != null) && item.IsOfType(StorageItemTypes.File))
                {
                    var storageFile = await folder.GetFileAsync(fileName);
                    var content = await storageFile.ReadBytesAsync();
                    return content;
                }

                return null;
            }).AsAsyncOperation();

        public static IAsyncOperation<byte[]> ReadBytesAsync(this StorageFile file) => Task.Run(async () =>
        {
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
                    using (var reader = new DataReader(stream.GetInputStreamAt(0)))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        var bytes = new byte[stream.Size];
                        reader.ReadBytes(bytes);
                        return bytes;
                    }
                }
            }

            return null;
        }).AsAsyncOperation();

        private static string GetFileName(string name) => string.Concat(name, FileExtension);
    }
}
