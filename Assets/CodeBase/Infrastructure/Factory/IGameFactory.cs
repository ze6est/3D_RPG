using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public GameObject CreatePlayer(GameObject at);
        public GameObject CreateHud();
        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
        public LootPiece CreateLoot();

        public void CleanUp();        
    }
}