using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Factory
{
    public interface IUIFactory : IService
    {
        public void CreateShop();
        public void CreateUIRoot();
    }
}