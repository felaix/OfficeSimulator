using UnityEngine;

[CreateAssetMenu(fileName = "NewPCData", menuName = "Upgrades/PC Data")]
public class PCData : UpgradeData
{
    [Header("PC Data")]
    public int processingPower; // Verarbeitungsgeschwindigkeit
    public int memory; // Arbeitsspeicher

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade(); // Zuerst die Basisklasse aufrufen


        // more logic here
        Debug.Log($"PC upgraded to level {currentLevel} with processing power: {processingPower} and memory: {memory}");
    }
}
