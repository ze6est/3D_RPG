using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.StaticData;
using UnityEngine;
using CodeBase.Infrastructure.StaticData.Windows;
using CodeBase.UI.Windows;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Ads;

namespace CodeBase.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";

        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressServise;
        private readonly IAdsService _adsService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressServise, IAdsService adsService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressServise = progressServise;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService, _progressServise);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}