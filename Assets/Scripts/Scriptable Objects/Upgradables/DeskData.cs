using UnityEngine;

[CreateAssetMenu(fileName = "NewDeskData", menuName = "Upgrades/Desk Data")]
public class DeskData : UpgradeData
{
    [Header("Desk Data")]
    public int comfort; // Gr��e des Arbeitsplatzes

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade();
        // Desk-spezifische Logik hinzuf�gen
        Debug.Log($"Desk upgraded to level {currentLevel} with comfort: {comfort}");
    }
}
