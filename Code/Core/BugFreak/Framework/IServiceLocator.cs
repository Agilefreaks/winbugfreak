using System.Collections.Generic;

namespace Bugfreak.Framework
{
    public interface IServiceLocator
    {
        TType GetService<TType>();

        IList<TType> GetServices<TType>();
    }
}