using UnityEngine;

[System.Serializable]
public class PlayerProjectStats
{
    [Header("Points")]
    public float DesignXP = 0.1f;
    public float DevXP = .1f;
    public float FameXP = .1f;
    public int Bugs = 0;
    public bool Completed = false;

    public PlayerProjectStrategy Strategies;

    public string Name = "Name";
    public int ID = -1;

    public PlayerProjectStats()
    {

    }

    public MarketingStrategy GetMarketingStrategy() { return this.Strategies.Marketing; }
    public EmployeePolicy GetEmployeePolicy() { return this.Strategies.Policy; }

    public void AddXP(float xp, bool design = false, bool dev = false, bool fame = false)
    {
        if (design) DesignXP += xp;
        if (dev) DevXP += xp;
        if (fame) FameXP += xp;
    }

    public void AddBug()
    {
        Bugs++;
    }

    public void RemoveBug()
    {
        Bugs--;
    }
}

[System.Serializable]
public class PlayerProjectStrategy
{
    public ProjectType Type = ProjectType.None;
    public MarketingStrategy Marketing = MarketingStrategy.None;
    public EmployeePolicy Policy = EmployeePolicy.None;

    public PlayerProjectStrategy(ProjectType type = ProjectType.None, MarketingStrategy marketing = MarketingStrategy.None, EmployeePolicy policy = EmployeePolicy.None)
    {
        this.Type = type;
        this.Marketing = marketing;
        this.Policy = policy;
    }
}
