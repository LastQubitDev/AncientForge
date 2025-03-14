using Code.StateMachine.GameStates;
using UnityEngine.Events;

namespace Code.StateMachine
{
    public class GameStateMachine
    {
        private IGameState _currentState;
        
        public readonly UnityAction InitGame = null;

        public GameStateMachine()
        {
            InitGame = () => ChangeState(new GameLaunchState());
        }
        
        public void ChangeState(IGameState newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}