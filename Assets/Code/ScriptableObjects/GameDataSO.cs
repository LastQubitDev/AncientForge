using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "ScriptableObjects/Gameplay/GameDataSO")]
    public class GameDataSO : ScriptableObject
    {
        [SerializeField] private AllItemsSO allItemsSO = null;

        public AllItemsSO AllItemsSO => allItemsSO;

        public void Initialize()
        {
            allItemsSO.Initialize();
        }
        
#if UNITY_EDITOR
        [ContextMenu("ValidateAllDataSO")]
        private void ValidateAllDataSO()
        {
            AllItemsSO.ValidateAndSetupData();
        }
#endif
    }
}
