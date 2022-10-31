using System;

namespace Splatrika.LongLongStep.Model
{
    public interface IObjectProviderService<T>
    {
        bool IsReady { get; }
        T Object { get; }

        event Action<T> Ready;
    }
}
