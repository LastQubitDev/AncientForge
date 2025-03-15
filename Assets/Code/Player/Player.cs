using Code.ScriptableObjects;

namespace Code.Player
{
    public class Player
    {
        public Inventory Inventory { get; } = new();

        public void Initialize(AllItemsSO allItemsSO)
        {
            Inventory.SetAllItemsSO(allItemsSO);
            Inventory.SetInitInventory(allItemsSO.GetInitialInventory());
        }
    }
}