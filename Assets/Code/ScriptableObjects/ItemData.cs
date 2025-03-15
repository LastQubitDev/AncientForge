using System;
using Code.Enums;
using ScriptableObjects.Gameplay;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Gameplay/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string rawItemKey = string.Empty;
        [SerializeField] private ItemType itemType = ItemType.Resource;
        [SerializeField] private Sprite itemIcon = null; //In ideal world this would be eg. an addressable reference for on-demand loading
    
        [Header("Translation keys")]
        [SerializeField] private string itemNameKey = string.Empty;
        [SerializeField] private string descriptionKey = string.Empty;

        [Header("Auto generated")]
        [SerializeField] private ItemKey itemKey;
        
        public ItemKey ItemKey => itemKey;
        public string RawItemKey => rawItemKey;
        public ItemType ItemType => itemType;
        public Sprite ItemIcon => itemIcon;

        public string ItemNameKey => itemNameKey;
        public string DescriptionKey => descriptionKey;

        public void SetItemEnumKey(string itemKeyString)
        {
            Enum.TryParse(itemKeyString, out itemKey);
            
            #if UNITY_EDITOR
                EditorUtility.SetDirty(this);
            #endif
        }
    }

    public struct ItemPackData
    {
        public readonly ItemKey ItemKey;
        public readonly ItemType ItemType;
        public readonly int Count;

        public ItemPackData(ItemKey itemKey, ItemType itemType, int count)
        {
            ItemKey = itemKey;
            ItemType = itemType;
            Count = count;
        }
    }
}