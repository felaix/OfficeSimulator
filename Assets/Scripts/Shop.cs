using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Transform shopContent; // Content-Bereich des Shop-UI
    public List<UpgradeData> data;

    public GameObject shopItemPrefab;

    private void Start()
    {
        LoadData();
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
