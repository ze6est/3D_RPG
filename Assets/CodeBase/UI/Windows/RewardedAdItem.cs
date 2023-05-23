using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private GameObject[] _adActiveObjects;
        [SerializeField] private GameObject[] _adInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _progressServise;

        public void Construct(IAdsService adsService, IPersistentProgressService progressServise)
        {
            _adsService = adsService;
            _progressServise = progressServise;
        }

        public void Initialize()
        {
            _showAdButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Subscribe() => 
            _adsService.RewardedWideoReady += OnRewardedWideoReady;

        public void Cleanup() => 
            _adsService.RewardedWideoReady -= OnRewardedWideoReady;

        private void OnShowAdClicked() => 
            _adsService.ShowRevardedVideo(OnVideoFinished);

        private void OnVideoFinished() => 
            _progressServise.Progress.WorldData.LootData.Add(_adsService.Reward);

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRevardedVideoReady();

            foreach (GameObject adActiveObjects in _adActiveObjects) 
            {
                adActiveObjects.SetActive(videoReady);
            }

            foreach (GameObject adInactiveObjects in _adInactiveObjects)
            {
                adInactiveObjects.SetActive(!videoReady);
            }
        }

        private void OnRewardedWideoReady() => 
            RefreshAvailableAd();
    }
}