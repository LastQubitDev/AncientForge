using Code.Managers;
using UnityEngine.Events;

namespace Code.StateMachine.GameStates
{
    public class GlobalInitializationState : State
    {
        private readonly GameManager _gameManager = null;
        
        public GlobalInitializationState(UnityAction onEnterFinished = null, UnityAction onExitFinished = null) : base(onEnterFinished, onExitFinished)
        {
            _gameManager = GameManager.Get;
            GameManager.Get.CustomLogger.Log($"Constructed {this}", CustomLogger.LogType.StateLog);
        }
        
        public override void Enter()
        {
            _gameManager.GameDataSo.Initialize();
            _gameManager.Player.Initialize(_gameManager.GameDataSo.AllItemsSO);
            
            base.Enter();
            GameManager.Get.CustomLogger.Log($"Entered {this}", CustomLogger.LogType.StateLog);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            base.Exit();
            GameManager.Get.CustomLogger.Log($"Exited {this}", CustomLogger.LogType.StateLog);
        }
    }
}