using System;

namespace CodeBase.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        public int Reward { get; }

        public event Action RewardedWideoReady;

        public void Initialize();
        public bool IsRevardedVideoReady();
        public void ShowRevardedVideo(Action onVideoFinished);
    }
}