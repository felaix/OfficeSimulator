using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCShop : MonoBehaviour
{
    public Transform shopContent; // Content-Bereich des Shop-UI
    public List<PCData> pcData;

    public GameObject shopItemPrefab;

    void Start()
    {
        
    }

    private void LoadPCData()
    {
        foreach (var item in pcData)
        {
            Instantiate(shopItemPrefab, shopContent, item);
        }
    }

    public void BuyPC()
    {

    }
}
