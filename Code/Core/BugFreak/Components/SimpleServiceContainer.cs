using System;
using System.Collections.Generic;
using System.Linq;
using BugFreak.Framework;

namespace BugFreak.Components
{
    internal class SimpleServiceContainer : IServiceLocator
    {
        private readonly Dictionary<Type, List<object>> _instanceDictionary;
        private readonly Dictionary<Type, List<Func<IServiceLocator, Type, object>>> _instanceCreatorDictionary;

        public SimpleServiceContainer()
        {
            _instanceDictionary = new Dictionary<Type, List<object>>();
            _instanceCreatorDictionary = new Dictionary<Type, List<Func<IServiceLocator, Type, object>>>();
        }

        public void AddService(Type type, object instance)
        {
            if (_instanceDictionary.ContainsKey(type))
            {
                _instanceDictionary[type].Add(instance);
            }
            else
            {
                _instanceDictionary.Add(type, new List<object> { instance });
            }
        }

        public void AddService(Type type, Func<IServiceLocator, Type, object> createFunc)
        {
            if (_instanceCreatorDictionary.ContainsKey(type))
            {
                _instanceCreatorDictionary[type].Add(createFunc);
            }
            else
            {
                _instanceCreatorDictionary.Add(type, new List<Func<IServiceLocator, Type, object>> { createFunc });
            }
        }

        public TType GetService<TType>()
        {
            object instance = null;
            var serviceType = typeof(TType);

            if (_instanceDictionary.ContainsKey(serviceType))
            {
                instance = _instanceDictionary[serviceType].FirstOrDefault();
            }
            else if (_instanceCreatorDictionary.ContainsKey(serviceType))
            {
                instance = _instanceCreatorDictionary[serviceType].First()(this, serviceType);
            }

            return (TType)instance;
        }

        public IList<TType> GetServices<TType>()
        {
            var serviceType = typeof(TType);

            var instances = GetValues(_instanceDictionary, serviceType)
                .Cast<TType>();

            var creations = GetValues(_instanceCreatorDictionary, serviceType)
                .Select(c => c(this, serviceType))
                .Cast<TType>();

            return instances.Union(creations).ToList();
        }

        private IEnumerable<T> GetValues<T>(Dictionary<Type, List<T>> dictionary, Type key)
        {
            return dictionary.Where(i => i.Key.IsAssignableFrom(key)).SelectMany(i => i.Value);
        }
    }
}