using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private UpgradeData upgradeData;

    private Button buyBtn;

    public void ApplyUpgradeData(UpgradeData data)
    {
        upgradeData = data;

        // Get & Set Icon
        if (transform.GetChild(0).TryGetComponent(out Image img)) { img.sprite = data.upgradeIcon; }

        // Get & Set Description
        if (transform.GetChild(1).TryGetComponent(out TMP_Text descriptionTMP)) { descriptionTMP.SetText(data.description); }

        // Get & Set Item Cost TMP
        if (transform.GetChild(2).GetChild(1).TryGetComponent(out TMP_Text costTMP)) { costTMP.SetText(data.cost.ToString()); }

        // Get & Listen to buy Btn
        Transform lastChild = transform.GetChild(transform.childCount - 1);
        if (lastChild.TryGetComponent(out Button btn)) { buyBtn = btn; } else { Debug.Log("No Button found"); }

        if (buyBtn != null)
        {
            buyBtn.onClick.AddListener(() => BuyUpgrade());
        }
    }

    private void BuyUpgrade()
    {
        if (PlayerInventory.Instance.PlayerStats.Money >= upgradeData.cost)
        {
            Debug.Log("Bought " + upgradeData.itemName);
            GameMessageBox.Instance.Show("Successfully bought " + upgradeData.itemName + " for $ " + upgradeData.cost.ToString() + ".", upgradeData.upgradeIcon, false);
            PlayerInventory.Instance.PlayerStats.Money -= upgradeData.cost;
            Shop.Instance.CreatePackage(upgradeData.itemObj);
        }
        else
        {
            Debug.Log("Not enough money!");
            GameMessageBox.Instance.ShowErrorNotEnoughMoney();
        }

        transform.parent.parent.gameObject.SetActive(false);
    }
}
