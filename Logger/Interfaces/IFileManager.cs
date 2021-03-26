using System.Threading.Tasks;

namespace Logger
{
    public interface IFileManager
    {
        Task WriteToFile(string message, string path, bool append = true);
        Task LogToConsole(string message);
        string NormalizePath(string path);
        string ResolvePath(string path);
    }
}
