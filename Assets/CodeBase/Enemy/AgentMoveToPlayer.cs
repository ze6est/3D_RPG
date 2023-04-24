using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private const float MinimalDistance = 1f;

        private Transform _playerTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.PlayerGameObject != null)
                InitializePlayerTransform();
            else
                _gameFactory.PlayerCreated += OnPlayerCreated;
        }

        private void Update()
        {
            if (Initialized() && PlayerNotReached())
                _agent.destination = _playerTransform.position;
        }

        private bool Initialized()
        {
            return _playerTransform != null;
        }

        private void OnPlayerCreated()
        {
            InitializePlayerTransform();
        }

        private void InitializePlayerTransform() => 
            _playerTransform = _gameFactory.PlayerGameObject.transform;

        private bool PlayerNotReached() =>
            Vector3.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;
    }
}