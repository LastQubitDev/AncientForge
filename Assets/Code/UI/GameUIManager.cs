using System;
using Code.Managers;
using UnityEngine;

namespace Code.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private InventoryViewController inventoryView = null;

        private void OnEnable()
        {
            BindListeners();
        }

        private void OnDisable()
        {
            UnbindListeners();
        }

        private void BindListeners()
        {
            GameManager.Get.Player.Inventory.OnInventoryChanged += inventoryView.UpdateInventoryEntries;
            GameManager.Get.Player.Inventory.TriggerAllInventoryRefresh();
        }

        private void UnbindListeners()
        {
            GameManager.Get.Player.Inventory.OnInventoryChanged -= inventoryView.UpdateInventoryEntries;
        }
    }
}
