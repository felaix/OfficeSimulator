using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    public Stats PlayerStats;


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

    private void Start()
    {
        CanvasManager.Instance.UpdatePlayerMoney(Color.white, PlayerStats.Money, PlayerStats.Money);
    }

    public void AddMoney(int amount)
    {
        PlayerStats.ModifyMoney(amount, true);
        CanvasManager.Instance.UpdatePlayerMoney(Color.green, PlayerStats.Money, amount);
    }

    public void DeductMoney(int amount)
    {
        PlayerStats.ModifyMoney(amount, false);
        CanvasManager.Instance.UpdatePlayerMoney(Color.red, PlayerStats.Money, amount);
    }
}
