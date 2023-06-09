﻿using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UI.Factory;
using CodeBase.UI.Services;
using CodeBase.Infrastructure.Services.Ads;

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
            RegisterAdsService();
            RegisterStaticData();

            IAssetProvider assetProvider = new AssetProvider();
            IRandomService randomService = new RandomService();
            IPersistentProgressService progressService = new PersistentProgressService();

            _container.RegisterSingle<IInputService>(InputService());
            _container.RegisterSingle<IAssetProvider>(assetProvider);
            _container.RegisterSingle<IPersistentProgressService>(progressService);

            _container.RegisterSingle<IUIFactory>(new UIFactory(assetProvider, _container.Single<IStaticDataService>(), progressService, _container.Single<IAdsService>()));
            _container.RegisterSingle<IWindowService>(new WindowService(_container.Single<IUIFactory>()));

            _container.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, _container.Single<IStaticDataService>(), randomService, progressService, _container.Single<IWindowService>()));
            _container.RegisterSingle<ISaveLoadService>(new SaveLoadService(_container.Single<IPersistentProgressService>(), _container.Single<IGameFactory>()));
        }

        private void RegisterAdsService()
        {
            var adsService = new AdsService();

            adsService.Initialize();

            _container.RegisterSingle<IAdsService>(adsService);
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
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