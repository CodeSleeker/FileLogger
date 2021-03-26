using System;
using static Logger.ServiceExtension;

namespace Logger
{
    public class BaseFileLogger : ILogger
    {
        public string FilePath { get; set; }
        public BaseFileLogger(string filePath)
        {
            FilePath = filePath;
        }
        public void Debug(string message, ErrorCode code)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");
            FileManager.LogToConsole($"[{currentTime}][DEBUG][{(int)code}] {message}{Environment.NewLine}");
        }

        public void Error(string message, ErrorCode code)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");
            FileManager.WriteToFile($"[{currentTime}][ERROR][{(int)code}] {message}{Environment.NewLine}", FilePath, append: true);
        }

        public void Info(string message, ErrorCode code)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");
            FileManager.WriteToFile($"[{currentTime}][INFO][{(int)code}] {message}{Environment.NewLine}", FilePath, append: true);
        }

        public void Warn(string message, ErrorCode code)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");
            FileManager.WriteToFile($"[{currentTime}][WARNING][{(int)code}] {message}{Environment.NewLine}", FilePath, append: true);
        }
    }
}
