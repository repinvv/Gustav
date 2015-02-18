namespace Gustav
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IoC
    {
        private readonly Dictionary<Type, RegisteredObject> registeredObjects = new Dictionary<Type, RegisteredObject>();

        public void Register<TType>() where TType : class
        {
            Register<TType, TType>(false, null);
        }

        public void Register<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(false, null);
        }

        public void RegisterSingleton<TType>() where TType : class
        {
            RegisterSingleton<TType, TType>();
        }

        public void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, null);
        }

        public void RegisterInstance<TType>(TType instance) where TType : class
        {
            RegisterInstance<TType, TType>(instance);
        }

        public void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, instance);
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
        }

        public object Resolve(Type type)
        {
            return ResolveObject(type);
        }

        private void Register<TType, TConcrete>(bool isSingleton, TConcrete instance)
        {
            Type type = typeof(TType);
            registeredObjects[type] = new RegisteredObject(typeof(TConcrete), isSingleton, instance);
        }

        public void Register(Type type, Type concrete)
        {
            registeredObjects[type] = new RegisteredObject(concrete, false, null);
        }

        public void Register(Type type)
        {
            registeredObjects[type] = new RegisteredObject(type, false, null);
        }

        private object ResolveObject(Type type)
        {
            RegisteredObject registeredObject;
            if (!registeredObjects.TryGetValue(type, out registeredObject))
            {
                throw new ArgumentOutOfRangeException(string.Format("The type {0} has not been registered", type.Name));
            }

            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.SingletonInstance != null)
            {
                return registeredObject.SingletonInstance;
            }

            var parameters = ResolveConstructorParameters(registeredObject);
            return registeredObject.CreateInstance(parameters.ToArray());
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            return constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType));
        }

        private class RegisteredObject
        {
            private readonly bool isSinglton;

            public RegisteredObject(Type concreteType, bool isSingleton, object instance)
            {
                isSinglton = isSingleton;
                ConcreteType = concreteType;
                SingletonInstance = instance;
            }

            public Type ConcreteType { get; private set; }
            public object SingletonInstance { get; private set; }

            public object CreateInstance(params object[] args)
            {
                object instance = Activator.CreateInstance(ConcreteType, args);
                if (isSinglton)
                {
                    SingletonInstance = instance;
                }

                return instance;
            }
        }
    }
}
