using CodeBase.Infrastructure.StaticData.Windows;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UI.StaticData;

namespace CodeBase.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        public LevelStaticData ForLevel(string sceneKey);
        public MonsterStaticData ForMonster(MonsterTypeId typeId);
        public WindowConfig ForWindow(WindowId shop);
        public void Load();
    }
}