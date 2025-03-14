using Code.Managers;
using UnityEngine.Events;

namespace Code.StateMachine.GameStates
{
    public class GameLaunchState : State
    {
        public GameLaunchState(UnityAction onEnterFinished = null, UnityAction onExitFinished = null) : base(onEnterFinished, onExitFinished)
        {
            GameManager.Get.CustomLogger.Log($"Constructed {this}", CustomLogger.LogType.StateLog);
        }
        
        public override void Enter()
        {
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