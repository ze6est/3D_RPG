using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 3f;

        private IGameFactory _gameFactory;
        private Transform _playerTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.PlayerCreated += OnPlayerCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack() { }

        private void OnAttackEnded() 
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack()
        {
            return !_isAttacking && CooldownIsUp();
        }

        private bool CooldownIsUp()
        {
            return _currentAttackCooldown <= 0;
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private void OnPlayerCreated()
        {
            _playerTransform = _gameFactory.PlayerGameObject.transform;
        }
    }
}