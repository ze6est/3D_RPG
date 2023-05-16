﻿using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;

        [SerializeField] private float _current;
        [SerializeField] private float _max;        

        public float Current { get => _current; set => _current = value; }
        public float Max { get => _max; set => _max = value; }

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            _current -= damage;

            HealthChanged?.Invoke();

            _animator.PlayHit();
        }
    }
}