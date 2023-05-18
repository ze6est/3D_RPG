using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class RandomService : IRandomService
    {
        public int Next(int lootMin, int lootMax) => 
            Random.Range(lootMin, lootMax);
    }
}