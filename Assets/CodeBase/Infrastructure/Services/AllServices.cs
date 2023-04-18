namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TServise>(TServise implementation) where TServise : IService => 
            Implementation<TServise>.ServiseInstanse = implementation;

        public TServise Single<TServise>() where TServise : IService =>
            Implementation<TServise>.ServiseInstanse;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiseInstanse;
        }
    }
}
