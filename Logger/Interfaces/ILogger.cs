namespace Logger
{
    public interface ILogger
    {
        void Info(string message, ErrorCode code);
        void Debug(string message, ErrorCode code);
        void Warn(string message, ErrorCode code);
        void Error(string message, ErrorCode code);
    }
}
