using UnityEngine;

[System.Serializable]
public class UpgradeData : ScriptableObject
{
    [Header("Upgrade Data")]
    public string itemName; // Name des Objekts
    public int currentLevel; // Aktueller Level des Upgrades
    public int maxLevel; // Maximal erreichbarer Level
    public int cost; // Kosten für das Upgrade
    public Sprite upgradeIcon; // Icon für das Upgrade
    public string description; // Beschreibung des Upgrades
    public GameObject itemObj;

    public virtual void ApplyUpgrade()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            Debug.Log($"{itemName} upgraded to level {currentLevel}.");
        }
        else
        {
            Debug.Log($"{itemName} has reached its maximum level!");
        }
    }
}
