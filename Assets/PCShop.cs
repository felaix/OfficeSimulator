using System.Collections.Generic;
using UnityEngine;

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
