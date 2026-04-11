using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Instance
    public static GameController Instance;
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
    
    [Header("Managers")]
    public InventoryManager inventory;
    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        inventory = InventoryManager.Instance;
        inventory.Initialize();
    }
}
