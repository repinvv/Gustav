namespace Gustav
{
    using System.Linq;
    using Gustav.RobotPosition;

    internal class Resolver
    {
        readonly IoC ioc = new IoC();

        public Resolver()
        {
            GetType().Assembly.GetTypes().ToList().ForEach(x=> ioc.Register(x));
            ioc.RegisterSingleton<RobotPositionStorage>();
            ioc.RegisterSingleton<RobotContainer>();
        }

        public T Get<T>()
        {
            return ioc.Resolve<T>();
        }
    }
}
