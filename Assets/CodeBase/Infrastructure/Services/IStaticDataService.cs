using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        LevelStaticData ForLevel(string sceneKey);
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();
    }
}