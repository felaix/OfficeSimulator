using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private PlayerStatsUIItem speedUI;
    [SerializeField] private PlayerStatsUIItem designUI;
    [SerializeField] private PlayerStatsUIItem devUI;
    [SerializeField] private PlayerStatsUIItem hungerUI;
    [SerializeField] private PlayerStatsUIItem corruptionUI;

    private Stats playerStats;
    private float animationDuration = 0.5f;

    private void OnEnable()
    {
        playerStats = PlayerInventory.Instance.PlayerStats;

        if (playerStats != null)
        {
            playerStats.OnStatsChanged += UpdateStats; // Subscribe
            UpdateStats(); // Initial update
        }
    }

    private void OnDisable()
    {
        if (playerStats != null)
            playerStats.OnStatsChanged -= UpdateStats; // Unsubscribe
    }


    public void UpdateStats()
    {
        Debug.Log("Updating playerstats...");

        AnimateStat(speedUI, playerStats.Speed);
        AnimateStat(designUI, playerStats.Design);
        AnimateStat(devUI, playerStats.Programming);
        AnimateStat(hungerUI, playerStats.Hunger);
        AnimateStat(corruptionUI, playerStats.Corruption);
    }

    private void AnimateStat(PlayerStatsUIItem uiItem, float newValue)
    {
        uiItem.slider.DOValue(newValue, animationDuration).SetEase(Ease.OutQuad);
        DOTween.To(() => float.Parse(uiItem.tmp.text),
                   x => uiItem.tmp.text = Mathf.RoundToInt(x).ToString(),
                   newValue,
                   animationDuration);
    }
}

[System.Serializable]
public class PlayerStatsUIItem
{
    public TMP_Text tmp;
    public Slider slider;
}
