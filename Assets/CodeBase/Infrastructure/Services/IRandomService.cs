namespace CodeBase.Infrastructure.Services
{
    public interface IRandomService : IService
    {
        public int Next(int lootMin, int lootMax);
    }
}