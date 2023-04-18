using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadServise : IService
    {
        public void SaveProgress();
        public PlayerProgress LoadProgress();
    }
}