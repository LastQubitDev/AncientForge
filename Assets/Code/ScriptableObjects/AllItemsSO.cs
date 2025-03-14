using System.Collections.Generic;
using System.IO;
using Code.Enums;
using Code.Managers;
using ScriptableObjects.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AllItemsSO", menuName = "ScriptableObjects/Gameplay/AllItemsSO")]
    public class AllItemsSO : ScriptableObject
    {
        [SerializeField] private ItemData[] resources = null;
        [SerializeField] private ItemData[] craftedItems = null;

        private readonly HashSet<ItemKey> _validItemEnumKeys = new();
        private const string ItemKeysEnumName = "ItemKey";

        public void Initialize()
        {
            _validItemEnumKeys.Clear();

            for (int i = 0; i < resources.Length; i++)
            {
                InitializeItemData(resources[i]);
            }

            for (int i = 0; i < craftedItems.Length; i++)
            {
                InitializeItemData(craftedItems[i]);
            }
        }

        private void InitializeItemData(ItemData itemData)
        {
            _validItemEnumKeys.Add(itemData.ItemKey);
        }
        
        public ItemDataReward[] GetInitialInventory()
        {
            return new[]
            {
                new ItemDataReward(ItemKey.IronOre, ItemType.Resource, Random.Range(3,6)),
                new ItemDataReward(ItemKey.GoldOre, ItemType.Resource, Random.Range(1,4)),
                new ItemDataReward(ItemKey.FireShard, ItemType.Resource, Random.Range(0,3)),
                new ItemDataReward(ItemKey.EmberDust, ItemType.Resource, Random.Range(0,3)),
                new ItemDataReward(ItemKey.DragonScale, ItemType.Resource, Random.Range(0,2))
            };
        }

        public bool IsValidItem(ItemKey itemEnumKey)
        {
            return _validItemEnumKeys.Contains(itemEnumKey);
        }

#if UNITY_EDITOR
        [ContextMenu("ValidateAndSetupData")]
        public void ValidateAndSetupData()
        {
            ValidateResources();
            ValidateCraftedItems();
            PrepareEnumFile();
        }

        private void ValidateResources()
        {
            foreach (var item in resources)
            {
                CheckItemType(item.name, item.RawItemKey, item.ItemType, ItemType.Resource);
            }
        }

        private void ValidateCraftedItems()
        {
            foreach (var item in craftedItems)
            {
                CheckItemType(item.name, item.RawItemKey, item.ItemType, ItemType.Crafted);
            }
        }

        private void CheckItemType(string soName, string itemKey, ItemType itemType, ItemType expectedType)
        {
            if (itemType != expectedType)
            {
                GameManager.Get.CustomLogger
                    .LogError($"Invalid item type for resource {itemKey} in SO named {soName}. Is {itemType}, expected {expectedType}", CustomLogger.LogType.Inventory);
            }
        }
        
        private void PrepareEnumFile()
        {
            string assetPath = AssetDatabase.GetAssetPath(this);
            string directoryPath = Path.GetDirectoryName(assetPath);
            string filePath = Path.Combine(directoryPath, $"{ItemKeysEnumName}.cs");

            int counter = 0;

            StringBuilder builder = new StringBuilder();
            builder.Append("namespace ScriptableObjects.Gameplay\n");
            builder.Append("{\n");
            builder.Append($"\tpublic enum {ItemKeysEnumName}\n\t{{\n");

            AddEnumEntries(builder, resources, ref counter);
            AddEnumEntries(builder, craftedItems, ref counter);
            
            builder.Append("\t}\n");
            builder.Append("}");
            
            File.WriteAllText(filePath, builder.ToString());
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(this);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            
        }

        private void AddEnumEntries(StringBuilder builder, ItemData[] itemData, ref int counter)
        {
            foreach (var item in itemData)
            {
                string itemEnumKey = ToTitleCase(item.RawItemKey.Replace("_", " ")).Replace(" ", "");
                item.SetItemEnumKey(itemEnumKey);
                builder.Append("\t\t");
                builder.Append(itemEnumKey);
                builder.Append($" = {counter},\n");
                counter++;
            }
        }
        
        private static string ToTitleCase(string input)
        {
            var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
#endif
    }
}