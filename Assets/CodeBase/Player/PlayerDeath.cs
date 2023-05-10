using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerAttack _attack;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private PlayerMove _move;
        [SerializeField] private GameObject _deathFx;

        private bool _isDeath;

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (!_isDeath && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDeath = true;

            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}