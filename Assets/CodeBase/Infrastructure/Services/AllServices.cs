using System;

namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TServise>(TServise implementation) where TServise : IServise => 
            Implementation<TServise>.ServiseInstanse = implementation;

        public TServise Single<TServise>() where TServise : IServise =>
            Implementation<TServise>.ServiseInstanse;

        private static class Implementation<TService> where TService : IServise
        {
            public static TService ServiseInstanse;
        }
    }
}
