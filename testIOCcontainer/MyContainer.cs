using System;
using System.Collections.Generic;
using System.Linq;
using testIOCcontainer.Strategy;

namespace testIOCcontainer
{
    public class MyContainer
    {
        private readonly Dictionary<Type, IResolveStrategy> _registrations = new();
        private readonly Dictionary<Type, object> _singletonRegistrations = new();

        public MyContainer()
        {
            _registrations = new Dictionary<Type, IResolveStrategy>();
            _singletonRegistrations = new Dictionary<Type, object>();
        }

        public void RegisterTransient<TAbs, TImp>()
        {
            if (!typeof(TAbs).IsAssignableFrom(typeof(TImp)))
                throw new InvalidOperationException("register a child and master class only");

            _registrations.Add(typeof(TAbs), new CreateNewConcreteObjectStrategy(_registrations, typeof(TImp)));
        }

        public void RegisterSingleton<TAbs, TImp>()
        {
            if (!typeof(TAbs).IsAssignableFrom(typeof(TImp)))
                throw new InvalidOperationException("register a child and master class only");
            
            _registrations
                .Add(typeof(TAbs), 
                    new CreateNewSingletonObjectStrategy(new MasterResolveStrategy(_registrations) ,_singletonRegistrations,typeof(TImp)));
            
        }

        public object GetResult(Type type)
        {
            var foundResolver = _registrations.TryGetValue(type, out var resolveStrategy);

            if (!foundResolver)
                throw new InvalidOperationException($"There is no registration for type: {type.ToString()}");
            return resolveStrategy.Resolve(type);
        }
    }
}