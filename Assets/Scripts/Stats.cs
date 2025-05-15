using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Design;
    public float Programming;
    public float Speed = 30f;

    public void IncreaseDesignSkill(float amount)
    {
        Design += amount;
    }

    public void IncreaseProgrammingSkill(float amount)
    {
        Programming += amount; 
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }

}
