using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private CharacterController _characterController;

        private static int _layerMask;

        private IInputService _inputService;       
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if(_inputService.IsAttackButtonUp() && !_animator.IsAttacking)
                _animator.PlayAttack();
        }

        public void OnAttack()
        {

        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(GetStartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 GetStartPoint()
        {
            return new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.PlayerStats;
        }
    }
}