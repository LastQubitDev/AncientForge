using Code.StateMachine.GameStates;
using UnityEngine.Events;

namespace Code.StateMachine
{
    public class GameStateMachine
    {
        private IGameState _currentState;
        
        public readonly UnityAction InitGameLaunch = null;
        public readonly UnityAction StartGame = null;

        public GameStateMachine()
        {
            InitGameLaunch = () => ChangeState(new GameLaunchState());
            StartGame = () => ChangeState(new GameState());
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