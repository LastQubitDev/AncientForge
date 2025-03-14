using Code.ScriptableObjects;
using Code.StateMachine;
using Code.StateMachine.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Code.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Get { get; private set; } = null;

        [SerializeField] private GameDataSO gameDataSO = null;
        [SerializeField] private CustomLogger customLogger = null;
        
        private GameStateMachine _gameStateMachine = null;
        private Player.Player _player = null;

        public GameDataSO GameDataSo => gameDataSO;
        public CustomLogger CustomLogger => customLogger;
        public Player.Player Player => _player;

        private void Awake()
        {
            if (Get != null && Get != this)
            {
                Destroy(this);
                return;
            }
            
            Get = this;
            Initialize();
            LoadGame();
        }

        private void Initialize()
        {
            _player = new Player.Player();
            _gameStateMachine = new GameStateMachine();
            
            _gameStateMachine.ChangeState(new GlobalInitializationState(this, onEnterFinished: _gameStateMachine.InitGame));
        }

        private void LoadGame()
        {
            SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Additive);
        }
    }
}
