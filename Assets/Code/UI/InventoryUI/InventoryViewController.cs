using System.Collections.Generic;
using Code.Managers;
using Code.ScriptableObjects;
using Code.UI.InventoryUI;
using ScriptableObjects.Gameplay;
using UnityEngine;

namespace Code.UI
{
    public class InventoryViewController : MonoBehaviour
    {
        [SerializeField] private Transform contentRoot = null;
        [SerializeField] private InventoryEntryView inventoryEntryPrefab = null;

        private readonly Dictionary<ItemKey, InventoryEntryView> _inventoryEntries = new();
        
        public void UpdateInventoryEntries(ItemPackData[] itemPackData)
        {
            for (int i = 0; i < itemPackData.Length; i++)
            {
                ItemPackData data = itemPackData[i];
                
                if (!_inventoryEntries.ContainsKey(data.ItemKey))
                {
                    Sprite itemIcon = GameManager.Get.GameDataSo.AllItemsSO.GetItemKeySprite(data.ItemKey);
                    _inventoryEntries.Add(data.ItemKey, Instantiate(inventoryEntryPrefab, contentRoot));
                    _inventoryEntries[data.ItemKey].Initialize(itemIcon);
                }
            
                _inventoryEntries[data.ItemKey].ReloadView(data);
            }
        }
    }
}
