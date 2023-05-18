using CodeBase.Infrastructure.StaticData;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using CodeBase.Infrastructure.Factory;
using CodeBase.Enemy;

namespace CodeBase.Logic.EnemySpawners
{
    public partial class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId { get; set; }
        public string Id { get; set; }

        [SerializeField] private bool _slain;

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory factory) => 
            _factory = factory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject monster = _factory.CreateMonster(MonsterTypeId, transform);
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