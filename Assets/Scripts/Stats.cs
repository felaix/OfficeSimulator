using System;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public float Design = 1.2f;
    public float Programming = 1.8f;
    public float Speed = 30f;
    public float Hunger = 100f;
    public float Corruption = 1f;
    public float Sleep = 100f;
    public int Money = 100;

    public event Action OnStatsChanged;

    private void NotifyChange() => OnStatsChanged?.Invoke();

    public void ModifySleep(float amount, bool increase = true)
    {
        Sleep += increase ? amount : -amount;
        Sleep = Mathf.Clamp(Sleep, 0, 100);
        NotifyChange();
    }

    public void ModifyMoney(int amount, bool increase = true)
    {
        Money += increase ? amount : -amount;
        if (Money < 0)
        {
            // ! TODO : Trigger Game over 
        }
        NotifyChange();
    }

    public void IncreaseDesignSkill(float amount)
    {
        Design += amount;
        NotifyChange();
    }

    public void IncreaseProgrammingSkill(float amount)
    {
        Programming += amount;
        NotifyChange();
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
        NotifyChange();
    }

    public void ModifyHunger(float amount, bool increase = true)
    {
        Hunger += increase ? amount : -amount;
        Hunger = Mathf.Clamp(Hunger, 0, 100);
        NotifyChange();
    }

    public void ModifyCorruption(float amount, bool increase = true)
    {
        Corruption += increase ? amount : -amount;
        Corruption = Mathf.Clamp(Corruption, 0, 100);
        NotifyChange();
    }
}
