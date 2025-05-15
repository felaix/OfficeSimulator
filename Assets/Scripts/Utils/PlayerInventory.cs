using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    public int money = 100;

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

    private void OnEnable()
    {
        CanvasManager.Instance.UpdatePlayerMoney(money);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        CanvasManager.Instance.UpdatePlayerMoney(money);
    }

    public void DeductMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            CanvasManager.Instance.UpdatePlayerMoney(money);
        }
        else
        {
            Debug.Log("Nicht genug Geld!");
        }
    }
}
