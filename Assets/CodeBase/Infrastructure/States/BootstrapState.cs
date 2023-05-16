using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _container;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _container = services;

            RegisterSetvices();
        }

        public void Enter()
        {            
            _sceneLoader.Load(Initial, EnterLoadProgress);
        }

        public void Exit()
        {

        }

        private void EnterLoadProgress() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterSetvices()
        {
            RegisterStaticData();

            _container.RegisterSingle<IInputService>(InputService());
            _container.RegisterSingle<IAssetProvider>(new AssetProvider());
            _container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>(), AllServices.Container.Single<IStaticDataService>()));
            _container.RegisterSingle<ISaveLoadService>(new SaveLoadService(_container.Single<IPersistentProgressService>(), _container.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _container.RegisterSingle(staticData);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}