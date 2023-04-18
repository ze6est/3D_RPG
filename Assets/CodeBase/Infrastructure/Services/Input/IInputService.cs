using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }

        public bool IsAttackButtonUp();
    }
}