using System.Collections.Generic;

namespace BugFreak.Framework
{
    public interface IServiceLocator
    {
        TType GetService<TType>();

        IList<TType> GetServices<TType>();
    }
}