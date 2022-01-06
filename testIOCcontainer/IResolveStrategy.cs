using System;

namespace testIOCcontainer
{
    public interface IResolveStrategy
    {
        object Resolve(Type type);
    }
}