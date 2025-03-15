using Code.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.InventoryUI
{
    public class InventoryEntryView : MonoBehaviour
    {
        [SerializeField] private Image itemIconImage = null;
        [SerializeField] private TextMeshProUGUI itemNameLabel = null;
        [SerializeField] private TextMeshProUGUI itemCountLabel = null;

        public void Initialize(Sprite itemIcon)
        {
            itemIconImage.sprite = itemIcon;
        }
        
        public void ReloadView(ItemPackData packData)
        {
            itemNameLabel.text = packData.ItemKey.ToString();
            itemCountLabel.text = packData.Count.ToString();
        }
    }
}
