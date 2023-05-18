using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        private const float Delay = 5f;

        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private Follow _follow;

        public event Action DeathChanged;

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
            _follow = GetComponent<Follow>();
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= OnHealthChanged;

            _animator.PlayDeath();
            _follow.enabled = false;
            SpawnDeathFx();

            DeathChanged?.Invoke();

            Destroy(gameObject, Delay);
        }

        private void SpawnDeathFx()
        {
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}