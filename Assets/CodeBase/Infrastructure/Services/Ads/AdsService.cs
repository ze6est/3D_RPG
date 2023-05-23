using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsService : IUnityAdsListener, IAdsService
    {
        private const string AngroidGameId = "5287091";
        private const string IOSGameId = "5287090";
        private const string RewardwdWideoPlacementId = "revardedVideo";

        private string _gameId;
        private Action _onVideoFinished;

        public int Reward => 7;

        public event Action RewardedWideoReady;

        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AngroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = AngroidGameId;
                    break;
                default:
                    Debug.Log("Unsapported platform for Ads");
                    break;
            }

            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId);
        }

        public void ShowRevardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(RewardwdWideoPlacementId);

            _onVideoFinished = onVideoFinished;
        }

        public bool IsRevardedVideoReady() =>
            Advertisement.IsReady(RewardwdWideoPlacementId);


        public void OnUnityAdsDidError(string message) =>
            Debug.Log($"OnUnityAdsDidError {message}");

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Skipped:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Finished:
                    _onVideoFinished?.Invoke();
                    break;
                default:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
            }

            _onVideoFinished = null;
        }

        public void OnUnityAdsDidStart(string placementId) =>
            Debug.Log($"OnUnityAdsDidStart {placementId}");

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady {placementId}");

            if (placementId == RewardwdWideoPlacementId)
                RewardedWideoReady?.Invoke();
        }
    }
}