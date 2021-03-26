using System;
using System.Runtime.CompilerServices;

namespace Logger
{
    public interface ILogFactory
    {
        void CreateFile(string path);
        event Action<(string message, ErrorCode code)> NewLog;
        bool IncludeOriginDetatils { get; set; }
        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);
        void Info(string message,
            ErrorCode code = ErrorCode.Application,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            bool IsIncludeOriginDetails = true);
        void Debug(string message,
            ErrorCode code = ErrorCode.Application,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            bool IsIncludeOriginDetails = true);
        void Warn(string message,
            ErrorCode code = ErrorCode.Application,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            bool IsIncludeOriginDetails = true);
        void Error(string message,
            ErrorCode code = ErrorCode.Application,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            bool IsIncludeOriginDetails = true);
    }
}
