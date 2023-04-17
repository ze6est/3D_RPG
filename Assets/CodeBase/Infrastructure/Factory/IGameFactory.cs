using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IServise
    {
        public GameObject CreatePlayer(GameObject at);
        public void CreateHud();
    }
}