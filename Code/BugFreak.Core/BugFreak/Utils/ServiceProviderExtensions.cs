using System;

namespace BugFreak.Utils
{
    public static class ServiceProviderExtensions
    {
        public static TType GetService<TType>(this IServiceProvider serviceProvider)
        {
            return (TType) serviceProvider.GetService(typeof (TType));
        }
    }
}
