using UnityEngine;

namespace Code.Managers
{
    public class CustomLogger : MonoBehaviour
    {
        public enum LogType
        {
            None = 0,
            StateLog = 1,
            Inventory = 2,
        }
        
        [SerializeField] private bool infoLogsEnabled = true;
        [SerializeField] private bool warningLogsEnabled = true;
        [SerializeField] private bool errorLogsEnabled = true;
        
        public void Log(string message, LogType logType = LogType.None)
        {
            if (!infoLogsEnabled)
            {
                return;
            }
            
            Debug.Log($"{logType.ToLogPrefix()}{message}");
        }

        public void LogWarning(string message, LogType logType = LogType.None)
        {
            if (!warningLogsEnabled)
            {
                return;
            }

            Debug.LogWarning($"{logType.ToLogPrefix()}{message}");
        }
        
        public void LogError(string message, LogType logType = LogType.None)
        {
            if (!errorLogsEnabled)
            {
                return;
            }

            Debug.LogError($"{logType.ToLogPrefix()}{message}");
        }
    }
}
