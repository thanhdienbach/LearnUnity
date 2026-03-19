using System.Collections.Generic;
using UnityEngine;

public class MiniInventoryManager : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    void Start()
    {
        AddItem("Thatch", 100);
        AddItem("Wood", 100);
        AddItem("Stone", 100);
        AddItem("Metal", 100);
        AddItem("Wood", 100);

        
        if (inventory.Count == 0) return;
        foreach (var pair in inventory)
        {
            Debug.Log($"Item: {pair.Key} | Amount: {pair.Value}" );
        }
    }

    private void AddItem(string itemName, int amount)
    {
        if (!inventory.TryAdd(itemName, amount)) inventory[itemName] += amount;
    }
}
