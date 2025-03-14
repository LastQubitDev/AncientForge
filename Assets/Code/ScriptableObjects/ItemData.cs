using System;
using Code.Enums;
using ScriptableObjects.Gameplay;
using UnityEngine;

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Gameplay/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string rawItemKey = string.Empty;
        [SerializeField] private ItemType itemType = ItemType.Resource;
    
        [Header("Translation keys")]
        [SerializeField] private string itemNameKey = string.Empty;
        [SerializeField] private string descriptionKey = string.Empty;

        private ItemKey _itemKey;
        
        public ItemKey ItemKey => _itemKey;
        public string RawItemKey => rawItemKey;
        public ItemType ItemType => itemType;
        
        public string ItemNameKey => itemNameKey;
        public string DescriptionKey => descriptionKey;

        public void SetItemEnumKey(string itemEnumKey)
        {
            Enum.TryParse(itemEnumKey, out _itemKey);
        }
    }

    public struct ItemDataReward
    {
        public ItemKey ItemKey;
        public ItemType ItemType;
        public int Count;

        public ItemDataReward(ItemKey itemKey, ItemType itemType, int count)
        {
            ItemKey = itemKey;
            ItemType = itemType;
            Count = count;
        }
    }
}