using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadServise;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadServise)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadServise = saveLoadServise;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew() => 
            _progressService.Progress = _saveLoadServise.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress() =>
            new PlayerProgress(initialLevel: "Main");
    }
}