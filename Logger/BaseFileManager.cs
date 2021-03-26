using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Logger
{
    public class BaseFileManager : IFileManager
    {
        public async Task LogToConsole(string message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(message);
            });
        }

        public string NormalizePath(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return path.Replace("/", "\\").Trim();
            else
                return path.Replace("\\", "/").Trim();
        }

        public string ResolvePath(string path)
        {
            return Path.GetFullPath(path);
        }

        public async Task WriteToFile(string message, string path, bool append = true)
        {
            path = NormalizePath(path);
            path = ResolvePath(path);
            await Awaiter.Async(nameof(BaseFileManager) + path, async () =>
            {
                await Task.Run(() =>
                {
                    using (var fileStream = (TextWriter)new StreamWriter(File.Open(path, append ? FileMode.Append : FileMode.Create)))
                        fileStream.Write(message);
                });
            });
        }
    }
}
