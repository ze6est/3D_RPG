using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private RewardedAdItem _adItem;

        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
            base.Construct(progressService);
            _adItem.Construct(adsService, progressService);
        }

        protected override void Initialize()
        {
            _adItem.Initialize();
            RefreshCoinText();
        }

        protected override void SubscribeUpdates()
        {
            _adItem.Subscribe();
            Progress.WorldData.LootData.Changed += RefreshCoinText;
        }

        protected override void Cleanup()
        {
            _adItem.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshCoinText;
        }

        private void RefreshCoinText() => 
            _coinText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}