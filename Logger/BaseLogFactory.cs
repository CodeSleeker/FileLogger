using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Logger
{
    public class BaseLogFactory : ILogFactory
    {
        public BaseLogFactory(ILogger[] loggers = null)
        {
            if(loggers != null)
            {
                foreach (var logger in loggers)
                    AddLogger(logger);
            }
        }
        protected static List<ILogger> Loggers = new List<ILogger>();
        protected object Lock = new object();
        public bool IncludeOriginDetatils { get; set; }

        public event Action<(string message, ErrorCode code)> NewLog = (details) => { };

        public void AddLogger(ILogger logger)
        {
            lock (Lock)
            {
                if (!Loggers.Contains(logger))
                {
                    Loggers.Add(logger);
                }
            }
        }

        public void CreateFile(string path)
        {
            new BaseLogFactory(new[] { new BaseFileLogger(path) });
        }

        public void Debug(string message, ErrorCode code = ErrorCode.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool IsIncludeOriginDetails = true)
        {
            if (IsIncludeOriginDetails)
                message = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - Line {lineNumber}] - Message: {message}";
            Loggers.ForEach(logger => logger.Debug(message, code));
            NewLog.Invoke((message, code));
        }

        public void Error(string message, ErrorCode code = ErrorCode.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool IsIncludeOriginDetails = true)
        {
            if (IsIncludeOriginDetails)
                message = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - Line {lineNumber}] - Message: {message}";
            Loggers.ForEach(logger => logger.Error(message, code));
            NewLog.Invoke((message, code));
        }

        public void Info(string message, ErrorCode code = ErrorCode.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool IsIncludeOriginDetails = true)
        {
            if (IsIncludeOriginDetails)
                message = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - Line {lineNumber}] - Message: {message}";
            Loggers.ForEach(logger => logger.Info(message, code));
            NewLog.Invoke((message, code));
        }

        public void RemoveLogger(ILogger logger)
        {
            lock (Lock)
            {
                if (Loggers.Contains(logger))
                    Loggers.Remove(logger);
            }
        }

        public void Warn(string message, ErrorCode code = ErrorCode.Application, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, bool IsIncludeOriginDetails = true)
        {
            if (IsIncludeOriginDetails)
                message = $"[{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.{Path.GetFileNameWithoutExtension(filePath)}.{origin} - Line {lineNumber}] - Message: {message}";
            Loggers.ForEach(logger => logger.Warn(message, code));
            NewLog.Invoke((message, code));
        }
    }
}
