using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public InventoryData inventoryData;
    private string filePath;

    private void Start()
    {
        LoadInventory();

        AddOrUpdateItem(5, "Mana Valid", 5, true);
        RemoveItem(3);
        AddOrUpdateItem(4, "Blood Valid", 5, true);
        AddOrUpdateItem(2, "Shield", 1, false);
    }

    private void LoadInventory()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "Inventory.json");
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonContent);
        }

        if (inventoryData == null)
        {
            inventoryData = new InventoryData();
        }
    }

    private void AddOrUpdateItem(int id, string itemName, int quantity, bool isStackable)
    {
        if (inventoryData == null || inventoryData.items == null) return;

        var existingItem = inventoryData.items.Find(x => x.id == id);
        if (existingItem != null &&  existingItem.isStackable && isStackable)
        {
            existingItem.quantity += quantity;
            Debug.Log($"Đã cộng dồn {quantity} vào {itemName}. Tổng {itemName} hiện có là {existingItem.quantity}");
        }
        else
        {
            inventoryData.items.Add(new ItemData(id, itemName, quantity, isStackable));
        }
        SaveInventory();
    }

    private void RemoveItem(int id)
    {
        inventoryData.items.RemoveAll(x => x.id == id);
        SaveInventory();
    }
    private void SaveInventory()
    {
        string json = JsonUtility.ToJson(inventoryData, true);
        File.WriteAllText(filePath, json);
    }
}

[Serializable]
public class ItemData
{
    public int id;
    public string itemName;
    public int quantity;
    public bool isStackable;

    public ItemData(int id, string itemName, int quantity, bool isStackable)
    {
        this.id = id;
        this.itemName = itemName;
        this.quantity = quantity;
        this.isStackable = isStackable;
    }
}

[Serializable]
public class InventoryData
{
    public List<ItemData> items = new List<ItemData>();
}
