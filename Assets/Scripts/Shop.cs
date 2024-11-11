using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    public Transform shopContent; 
    public Transform packageSpawnPoint; 
    public List<UpgradeData> data;

    public GameObject shopItemPrefab;
    public GameObject package;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    public void CreatePackage(GameObject item)
    {
        Package newPackage = Instantiate(package, packageSpawnPoint.position, Quaternion.identity).GetComponent<Package>();
        newPackage.item = item;
    }

    private void LoadData()
    {
        foreach (var item in data)
        {
            GameObject uiItem = Instantiate(shopItemPrefab, shopContent, item);
            ShopItem shopItem = uiItem.AddComponent<ShopItem>();
            shopItem.ApplyUpgradeData(item);
        }
    }
}
