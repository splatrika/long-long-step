using System;
using UnityEngine;

namespace Splatrika.LongLongStep.Model
{
    public class ObjectProviderService<T>
        : IObjectProviderService<T>, IRegisterObjectService<T>
    {
        public bool IsReady => Object != null;
        public T Object { get; private set; }

        private ILogger _logger { get; }

        public event Action<T> Ready;


        public ObjectProviderService(ILogger logger)
        {
            _logger = logger;
        }


        public void Register(T @object)
        {
            if (IsReady)
            {
                _logger.LogError(nameof(ObjectProviderService<T>),
                    $"{nameof(Register)}: " +
                    $"Object is already registered");
                return;
            }
            Object = @object;
            Ready?.Invoke(Object);
        }
    }
}
