using System;

namespace CodeBase.Data
{
    [Serializable]
    public partial class PlayerProgress
    {
        public WorldData WorldData;
        public State PlayerState;
        public Stats PlayerStats;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new State();
            PlayerStats = new Stats();
            KillData = new KillData();
        }
    }
}