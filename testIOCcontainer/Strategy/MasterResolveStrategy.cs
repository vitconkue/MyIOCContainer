using System;
using System.Collections.Generic;

namespace testIOCcontainer.Strategy
{
    internal class MasterResolveStrategy : IResolveStrategy
    {
        private Dictionary<Type, IResolveStrategy> _registrations;

        public MasterResolveStrategy(Dictionary<Type, IResolveStrategy> registrations)
        {
            _registrations = registrations;
        }

        public object Resolve(Type type)
        {
            if (_registrations.TryGetValue(type, out var resolveStrategy)) return resolveStrategy.Resolve(type);
            if (!type.IsAbstract)
            {
                var concreteStrategy = new CreateNewConcreteObjectStrategy(_registrations, type);
                // dummy argument
                return concreteStrategy.Resolve(type);
            }

            throw new InvalidOperationException($"There is no registration for type: {type.ToString()}");
            
        }
    }
}