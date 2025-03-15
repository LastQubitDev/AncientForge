using System.Collections.Generic;
using Code.Enums;
using Code.Managers;
using Code.ScriptableObjects;
using ScriptableObjects.Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Player
{
    public class Inventory
    {
        private readonly Dictionary<ItemType, Dictionary<ItemKey, int>> _itemsData = new();
        private AllItemsSO AllItemsSO { get; set; } = null;

        public UnityAction<ItemPackData[]> OnInventoryChanged = null;
        
        public void SetAllItemsSO(AllItemsSO allItemsSO)
        {
            AllItemsSO = allItemsSO;
        }
        
        public void SetInitInventory(ItemPackData[] initialInventoryPack)
        {
            foreach (var itemData in initialInventoryPack)
            {
                if (!AllItemsSO.IsValidItem(itemData.ItemKey))
                {
                    GameManager.Get.CustomLogger
                        .LogError($"Invalid entry {itemData.ItemKey} in initial inventory data, skipping", CustomLogger.LogType.Inventory);
                    
                    continue;
                }
                
                AddItem(itemData, notifyInventoryChanged: false);
                OnInventoryChanged?.Invoke(initialInventoryPack);
            }
        }

        public void TriggerAllInventoryRefresh()
        {
            List<ItemPackData> allPacks = new List<ItemPackData>();
            
            foreach (KeyValuePair<ItemType, Dictionary<ItemKey, int>> typePair in _itemsData)
            {
                foreach (var itemKeyPair in typePair.Value)
                {
                    allPacks.Add(new ItemPackData(itemKeyPair.Key, typePair.Key, itemKeyPair.Value));
                }
            }
            
            OnInventoryChanged?.Invoke(allPacks.ToArray());
        }
        
        public void AddItem(ItemPackData itemPack, bool notifyInventoryChanged = true)
        {
            if (itemPack.Count == 0)
            {
                GameManager.Get.CustomLogger.LogWarning
                (
                    $"Attempted to add 0 of {itemPack.ItemKey}, skipping operation",
                    CustomLogger.LogType.Inventory
                );
                
                return;
            }
            
            PrepareItemDataSupport(itemPack);
            _itemsData[itemPack.ItemType][itemPack.ItemKey] += itemPack.Count;

            if (notifyInventoryChanged)
            {
                OnInventoryChanged?.Invoke(new []{itemPack});
            }
        }

        private void PrepareItemDataSupport(ItemPackData itemPack)
        {
            if (!_itemsData.ContainsKey(itemPack.ItemType))
            {
                _itemsData.Add(itemPack.ItemType, new Dictionary<ItemKey, int>());
            }

            if (!_itemsData[itemPack.ItemType].ContainsKey(itemPack.ItemKey))
            {
                _itemsData[itemPack.ItemType][itemPack.ItemKey] = 0;
            }
        }

        public void RemoveItem(ItemPackData itemPack)
        {
            if (!_itemsData.ContainsKey(itemPack.ItemType) || !_itemsData[itemPack.ItemType].ContainsKey(itemPack.ItemKey))
            {
                GameManager.Get.CustomLogger.LogError
                (
                    $"Trying to remove invalid resources for item pack {itemPack.ItemType} - {itemPack.ItemKey}", 
                    CustomLogger.LogType.Inventory
                );
                
                return;
            }

            if (itemPack.Count > _itemsData[itemPack.ItemType][itemPack.ItemKey])
            {
                GameManager.Get.CustomLogger.LogError
                (
                    $"Tried to remove too many resources of {itemPack.ItemKey}, count requested {itemPack.Count}", 
                    CustomLogger.LogType.Inventory
                );
            }

            int resValue = _itemsData[itemPack.ItemType][itemPack.ItemKey];
            _itemsData[itemPack.ItemType][itemPack.ItemKey] = Mathf.Clamp(resValue - itemPack.Count, 0, int.MaxValue);
            
            OnInventoryChanged?.Invoke(new []{itemPack});
        }
    }
}