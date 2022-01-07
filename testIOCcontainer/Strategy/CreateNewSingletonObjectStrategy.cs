using System;
using System.Collections.Generic;

namespace testIOCcontainer.Strategy
{
    internal class CreateNewSingletonObjectStrategy: IResolveStrategy
    {
        private readonly Dictionary<Type, object> _singletonRegistrations;
        private readonly Type _targetType;
        private readonly MasterResolveStrategy _masterResolveStrategy;

        public CreateNewSingletonObjectStrategy(MasterResolveStrategy masterResolveStrategy ,Dictionary<Type,object> singletonRegistrations, Type targetType)
        {
            _singletonRegistrations = singletonRegistrations;
            _targetType = targetType;
            _masterResolveStrategy = masterResolveStrategy;
        }

        public object Resolve(Type type)
        {
            if (!_singletonRegistrations.TryGetValue(type, out var realObject))
            {
                var newObject = _masterResolveStrategy.Resolve(_targetType); 
                _singletonRegistrations.Add(type, newObject);
                return newObject;
            }

            return realObject;
        }
    }
}