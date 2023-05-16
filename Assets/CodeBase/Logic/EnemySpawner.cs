using CodeBase.Infrastructure.StaticData;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Factory;
using CodeBase.Enemy;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        [SerializeField] private bool _slain;

        private string _id;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(_monsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.DeathChanged += OnDeathChanged;
        }

        private void OnDeathChanged()
        {
            if(_enemyDeath != null)
                _enemyDeath.DeathChanged -= OnDeathChanged;

            Slay();
        }

        private void Slay()
        {
            _slain = true;
        }
    }
}