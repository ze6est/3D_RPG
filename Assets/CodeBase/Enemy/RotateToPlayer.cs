﻿using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToPlayer : Follow
    {
        [SerializeField] private float _speed;

        private Transform _playerTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (IsPlayerExist())
                InitializeHeroTransform();
            else
                _gameFactory.PlayerCreated += OnPlayerCreated;
        }

        private void Update()
        {
            if (IsInitialized())
                RotateTowardsHero();
        }

        private void OnDestroy()
        {
            if (_gameFactory != null)
                _gameFactory.PlayerCreated -= OnPlayerCreated;
        }

        private bool IsPlayerExist() =>
          _gameFactory.PlayerGameObject != null;

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDelta = _playerTransform.position - transform.position;
            _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
          Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 position) =>
          Quaternion.LookRotation(position);

        private float SpeedFactor() =>
          _speed * Time.deltaTime;

        private bool IsInitialized() =>
          _playerTransform != null;

        private void OnPlayerCreated() =>
          InitializeHeroTransform();

        private void InitializeHeroTransform() =>
          _playerTransform = _gameFactory.PlayerGameObject.transform;
    }
}