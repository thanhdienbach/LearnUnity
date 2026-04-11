using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public InventoryData inventoryData;

    private void Start()
    {
        LoadInventory();
    }

    private void LoadInventory()
    {
        Debug.Log("Path: " + Application.streamingAssetsPath + "/Inventory.json");
        string filePath = Path.Combine(Application.streamingAssetsPath, "Inventory.json");
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonContent);
        }
        else
        {
            Debug.Log("Không tìm thấy file tại: " + "/" + filePath);
        }
    }
}

[Serializable]
public class ItemData
{
    public int id;
    public string itemName;
    public int quantity;
    public bool isStackable;
}

[Serializable]
public class InventoryData
{
    public List<ItemData> items;
}
