using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        private const float MinimumVelocity = 0.1f;
        
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int WinHash = Animator.StringToHash("Win");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _walkingStateHash = Animator.StringToHash("Move");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");       
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        private readonly int _victoryStateHash = Animator.StringToHash("Victory");
        private readonly int _hitStateHash = Animator.StringToHash("GetHit");


        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;

        private void Update()
        {
            if (ShouldMove())
                Move(_characterController.velocity.magnitude);
            else
                StopMoving();
        }

        public void PlayAttack() => 
            _animator.SetTrigger(AttackHash);

        public void PlayDeath() => 
            _animator.SetTrigger(DieHash);

        public void PlayHit() => 
            _animator.SetTrigger(HitHash);

        public void ResetToIdle() => 
            _animator.Play(_idleStateHash, -1);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => 
            StateExited?.Invoke(StateFor(stateHash));

        private void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        private void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        private bool ShouldMove() =>
            _characterController.velocity.magnitude > MinimumVelocity;

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _walkingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}