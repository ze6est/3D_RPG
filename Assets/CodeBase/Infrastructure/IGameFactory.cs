using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory
    {
        public GameObject CreatePlayer(GameObject at);
        public void CreateHud();
    }
}