using System;
using System.Collections.Generic;
using System.Linq;

namespace testIOCcontainer.Strategy
{
    internal class CreateNewConcreteObjectStrategy : IResolveStrategy
    {
        private Type _targetType;
        private MasterResolveStrategy _masterResolveStrategy;

        public CreateNewConcreteObjectStrategy(Dictionary<Type, IResolveStrategy> registrations, Type targetType)
        {
            _masterResolveStrategy = new MasterResolveStrategy(registrations);
            _targetType = targetType;
        }

        // dummy argument 
        public object Resolve(Type dummy)
        {
            var constructorInfos = _targetType.GetConstructors();
            if (constructorInfos.Length == 0) return Activator.CreateInstance(_targetType);
            // find the least parameters constructor
            var chosenConstructor = constructorInfos
                .Aggregate(
                    (before, after) => before.GetParameters().Length < after.GetParameters().Length ? before : after);
            var leastNumberOfConstructor = chosenConstructor.GetParameters().Length;

            if (leastNumberOfConstructor == 0) return Activator.CreateInstance(_targetType);


            var paramTypes = chosenConstructor.GetParameters().Select(para => para.ParameterType).ToList();

            var realConstructorObjects = new List<object>();
            foreach (var paramType in paramTypes)
            {
                Console.WriteLine($"Found type {paramType} in constructor of {_targetType}");
                var realObject = _masterResolveStrategy.Resolve(paramType);
                realConstructorObjects.Add(realObject);
            }

            return Activator.CreateInstance(_targetType, realConstructorObjects.ToArray());
        }
    }
}