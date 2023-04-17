﻿using UnityEngine;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}