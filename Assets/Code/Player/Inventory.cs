using System.Collections.Generic;
using System.Xml;
using Code.Enums;
using Code.Managers;
using Code.ScriptableObjects;
using ScriptableObjects.Gameplay;
using UnityEngine;

namespace Code.Player
{
    public class Inventory
    {
        private readonly Dictionary<ItemType, Dictionary<ItemKey, int>> _resourcesAndCraftedData = new();
        private AllItemsSO AllItemsSO { get; set; } = null;

        public void SetAllItemsSO(AllItemsSO allItemsSO)
        {
            AllItemsSO = allItemsSO;
        }
        
        public void SetInitInventory(ItemDataReward[] initialInventory)
        {
            foreach (var itemData in initialInventory)
            {
                if (!AllItemsSO.IsValidItem(itemData.ItemKey))
                {
                    GameManager.Get.CustomLogger
                        .LogError($"Invalid entry {itemData.ItemKey} in initial inventory data, skipping", CustomLogger.LogType.Inventory);
                    
                    continue;
                }
                
                AddItem(itemData);
                //Debug.LogError(JsonConvert.SerializeObject(_resourcesAndCraftedData, Formatting.Indented));
            }
        }
        
        public void AddItem(ItemDataReward item)
        {
            PrepareItemDataSupport(item);
            _resourcesAndCraftedData[item.ItemType][item.ItemKey] += item.Count;
        }

        private void PrepareItemDataSupport(ItemDataReward item)
        {
            if (!_resourcesAndCraftedData.ContainsKey(item.ItemType))
            {
                _resourcesAndCraftedData.Add(item.ItemType, new Dictionary<ItemKey, int>());
            }

            if (!_resourcesAndCraftedData[item.ItemType].ContainsKey(item.ItemKey))
            {
                _resourcesAndCraftedData[item.ItemType][item.ItemKey] = 0;
            }
        }

        public void RemoveItem(int itemId)
        {
            //items.RemoveAll(item => item.Id == itemId);
            //Console.WriteLine($"Removed item with ID: {itemId}");
        }
    }
}