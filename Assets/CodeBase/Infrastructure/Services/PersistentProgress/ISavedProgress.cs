﻿using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }

    public interface ISavedProgressReader
    {        
        public void LoadProgress(PlayerProgress progress);
    }
}