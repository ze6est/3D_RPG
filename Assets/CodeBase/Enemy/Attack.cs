using CodeBase.Logic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        private const float DrawSeconds = 1f;
        private const string LayerPlayer = "Player";

        [SerializeField] private EnemyAnimator _animator;
        public float AttackCooldown = 3f;
        public float AttackCleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10f;
        
        private Transform _playerTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        public void Construct(Transform playerTransform) => 
            _playerTransform = playerTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer(LayerPlayer);            
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
                PhysicsDebug.DrawDebug(GetStartPoint(), AttackCleavage, DrawSeconds);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded() 
        {
            _currentAttackCooldown = AttackCooldown;
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
            int hitsCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), AttackCleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
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
    }
}