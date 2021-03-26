using Microsoft.Extensions.DependencyInjection;

namespace Logger
{
    public class ServiceExtension
    {
        public static ServiceProvider serviceProvider = new ServiceCollection()
            .AddTransient<IFileManager, BaseFileManager>()
            .AddTransient<ILogFactory, BaseLogFactory>()
            .BuildServiceProvider();
        public static IFileManager FileManager = serviceProvider.GetService<IFileManager>();
        public static ILogFactory LogManager = serviceProvider.GetService<ILogFactory>();
    }
}
