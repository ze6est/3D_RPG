using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.StaticData;
using UnityEngine;
using CodeBase.Infrastructure.StaticData.Windows;
using CodeBase.UI.Windows;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";

        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressServise;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressServise)
        {
            _assets = assets;
            _staticData = staticData;
            _progressServise = progressServise;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressServise);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}