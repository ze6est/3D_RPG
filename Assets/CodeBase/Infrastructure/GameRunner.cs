using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

            if(bootstrapper == null ) { }
                Instantiate(_bootstrapperPrefab);
        }
    }
}