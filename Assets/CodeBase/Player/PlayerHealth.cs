using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private PlayerAnimator _animator;

        private State _state;

        public Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHP;
            private set
            {
                if(_state.CurrentHP != value)
                {
                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }
        public float Max
        {
            get => _state.MaxHP;
            private set => _state.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHP = Current;
            progress.PlayerState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            _animator.PlayHit();
        }
    }
}