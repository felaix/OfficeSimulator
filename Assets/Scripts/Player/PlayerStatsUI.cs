using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsUIItem speedUI;
    [SerializeField] private PlayerStatsUIItem designUI;
    [SerializeField] private PlayerStatsUIItem devUI;
    [SerializeField] private PlayerStatsUIItem hungerUI;
    [SerializeField] private PlayerStatsUIItem corruptionUI;

    private Stats playerStats;

    private void OnEnable()
    {
        playerStats = PlayerInventory.Instance.PlayerStats;

        if (playerStats != null) UpdateStats();
    }

    public void UpdateStats()
    {
        Debug.Log("Updating playerstats...");

        speedUI.tmp.text = playerStats.Speed.ToString();
        designUI.tmp.text = playerStats.Design.ToString();
        devUI.tmp.text = playerStats.Programming.ToString();
        hungerUI.tmp.text = playerStats.Hunger.ToString();

        speedUI.slider.value = playerStats.Speed;
        designUI.slider.value = playerStats.Design;
        devUI.slider.value = playerStats.Programming;
        hungerUI.slider.value = playerStats.Hunger;

        float corruptionXP = (playerStats.Speed + playerStats.Design + playerStats.Programming - playerStats.Hunger);
        corruptionUI.tmp.text = corruptionXP.ToString();
        corruptionUI.slider.value = corruptionXP;
    }

}

[System.Serializable]
public class PlayerStatsUIItem
{
    public TMP_Text tmp;
    public Slider slider;
}
