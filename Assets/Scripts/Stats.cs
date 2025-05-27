
using System.Diagnostics;

[System.Serializable]
public class Stats
{
    public float Design = 1.2f;
    public float Programming = 1.8f;
    public float Speed = 30f;
    public float Hunger = .1f;
    public float Corruption = 1f;
    public float Sleep = 100f;
    public int Money = 100;

    public void ModifySleep(float amount, bool increase = true)
    {
        if (increase) Sleep += amount;
        else Sleep -= amount;
        if (Sleep > 100)
        {
            Sleep = 100;
        }else if (Sleep < 0)
        {
            Sleep = 0;
        }
    }

    public void ModifyMoney(int amount, bool increase = true)
    {
        if (increase) Money += amount;
        else
        {
             Money -= amount;
        }

        if (Money < 0)
        {
            // ! TODO : Trigger Game over 
        }
    }

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
