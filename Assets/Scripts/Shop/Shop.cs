using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    public Transform shopContent;
    public Transform packageSpawnPoint;
    public GameObject shopItemPrefab;
    public GameObject package;

    private List<UpgradeData> data;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadUpgradeData();
        LoadData();
    }

    private void LoadUpgradeData()
    {
        // Voraussetzung: Alle UpgradeData ScriptableObjects liegen im "Resources/Upgrades" Ordner
        data = new List<UpgradeData>(Resources.LoadAll<UpgradeData>("Items"));
        Debug.Log($"Loaded {data.Count} UpgradeData assets.");
    }

    private void LoadData()
    {
        foreach (var item in data)
        {
            GameObject uiItem = Instantiate(shopItemPrefab, shopContent);
            ShopItem shopItem = uiItem.AddComponent<ShopItem>();
            shopItem.ApplyUpgradeData(item);
        }
    }

    public void CreatePackage(GameObject item)
    {
        Package newPackage = Instantiate(package, packageSpawnPoint.position, Quaternion.identity).GetComponent<Package>();
        newPackage.item = item;
    }
}
