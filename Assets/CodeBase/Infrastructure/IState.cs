namespace CodeBase.Infrastructure
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}