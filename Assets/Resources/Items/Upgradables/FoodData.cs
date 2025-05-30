using UnityEngine;

[CreateAssetMenu(fileName = "New Food Data", menuName = "Upgrades/Food Data")]
public class FoodData : UpgradeData
{
    [Header("Food Data")]
    public float hunger;

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade();

        // more logic here
    }
}
