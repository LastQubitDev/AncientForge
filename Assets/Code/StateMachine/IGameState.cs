namespace Code.StateMachine
{
    public interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
    }
}