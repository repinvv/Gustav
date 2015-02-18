namespace Gustav
{
    using System.Linq;
    using Gustav.Storage;

    internal class Resolver
    {
        readonly IoC ioc = new IoC();

        public Resolver()
        {
            GetType().Assembly.GetTypes().ToList().ForEach(x=> ioc.Register(x));
            ioc.RegisterSingleton<EnemyDataStorage>();
            ioc.RegisterSingleton<CombatParametersStorage>();
        }

        public T Get<T>()
        {
            return ioc.Resolve<T>();
        }
    }
}
