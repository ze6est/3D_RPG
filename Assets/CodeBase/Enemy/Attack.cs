using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Player;
using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        private const float DrawSeconds = 1f;
        private const string LayerPlayer = "Player";

        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _cleavage = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _damage = 10f;

        private IGameFactory _gameFactory;
        private Transform _playerTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            _layerMask = 1 << LayerMask.NameToLayer(LayerPlayer);

            _gameFactory.PlayerCreated += OnPlayerCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack() 
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(GetStartPoint(), _cleavage, DrawSeconds);
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(_damage);
            }
        }

        private void OnAttackEnded() 
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        private bool Hit(out Collider hit)
        {            
            int hitsCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), _cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _effectiveDistance;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack()
        {
            return _attackIsActive && !_isAttacking && CooldownIsUp();
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