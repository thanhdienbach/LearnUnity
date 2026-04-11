using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public InventoryData inventoryData;
    private string _filePath;

    #region Instance
    public static InventoryManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Initialize
    private bool _initialized = false;
    public void Initialize()
    {
        if (_initialized) return;
        SetUpData();
        LoadInventory();
        _initialized = true;
    }
    private void SetUpData()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "Inventory.json");
        var fileInfo = new FileInfo(_filePath);
        if (fileInfo.Exists && fileInfo.Length != 0) return;
        var sourcePath = Path.Combine(Application.streamingAssetsPath, "Inventory.json");
        if (!File.Exists(sourcePath)) return;
        File.Copy(sourcePath, _filePath, true);
        Debug.Log($"Đã copy file từ {sourcePath} sang {_filePath}");
    }
    private void LoadInventory()
    {
        if (File.Exists(_filePath))
        {
            string jsonContent = File.ReadAllText(_filePath);
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonContent);
        }
        if (inventoryData == null)
        {
            inventoryData = new InventoryData();
        }
    }
    #endregion
    
    
    public void AddOrUpdateItem(int id, string itemName, int quantity, bool isStackable)
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
        File.WriteAllText(_filePath, json);
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
