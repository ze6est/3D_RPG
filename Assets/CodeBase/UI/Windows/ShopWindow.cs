using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _coinText;

        protected override void Initialize() => 
            RefreshCoinText();

        protected override void SubscribeUpdates() => 
            Progress.WorldData.LootData.Changed += RefreshCoinText;

        protected override void Cleanup() => 
            Progress.WorldData.LootData.Changed -= RefreshCoinText;

        private void RefreshCoinText() => 
            _coinText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}