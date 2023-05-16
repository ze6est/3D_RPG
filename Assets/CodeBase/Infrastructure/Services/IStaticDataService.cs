using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void LoadMonsters();
    }
}