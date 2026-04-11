using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [ContextMenu("Pick Coin")]
    public void PickCoins()
    {
        InventoryManager.Instance.AddOrUpdateItem(10, "Gold Coin", 1, true);
    }
}
