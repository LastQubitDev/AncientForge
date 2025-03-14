using UnityEngine.Events;

namespace Code.StateMachine
{
    public abstract class State : IGameState
    {
        private readonly UnityAction _onEnterFinished;
        private readonly UnityAction _onExitFinished;

        protected State(UnityAction onEnterFinished = null, UnityAction onExitFinished = null)
        {
            _onEnterFinished = onEnterFinished;
            _onExitFinished = onExitFinished;
        }
        
        public virtual void Enter()
        {
            _onEnterFinished?.Invoke();
        }
        
        public virtual void Exit()
        {
            _onExitFinished?.Invoke();
        }
        
        public abstract void Update();
    }
}