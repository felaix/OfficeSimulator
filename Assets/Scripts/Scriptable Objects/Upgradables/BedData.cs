using UnityEngine;

[CreateAssetMenu(fileName = "NewBedData", menuName = "Upgrades/Bed Data")]
public class BedData : UpgradeData
{
    [Header("Bed Data")]
    public int comfort; 

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade();
        // Desk-spezifische Logik hinzufügen
        Debug.Log($"Bed upgraded to level {currentLevel} with comfort: {comfort}");
    }
}
