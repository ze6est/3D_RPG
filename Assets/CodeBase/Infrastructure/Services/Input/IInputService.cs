using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }

        public bool IsAttackButtonUp();
    }
}