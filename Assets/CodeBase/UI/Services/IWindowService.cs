using CodeBase.Infrastructure.Services;
using CodeBase.UI.StaticData;

namespace CodeBase.UI.Services
{
    public interface IWindowService : IService
    {
        public void Open(WindowId windowId);
    }
}