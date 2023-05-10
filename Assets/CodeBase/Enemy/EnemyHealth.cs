using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;

        private float _current;
        private float _max;

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            _current -= damage;

            HealthChanged?.Invoke();

            _animator.PlayHit();
        }
        
    }
}