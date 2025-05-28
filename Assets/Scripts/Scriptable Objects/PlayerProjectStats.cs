using UnityEngine;

[System.Serializable]
public class PlayerProjectStats
{
    [Header("Points")]
    public float DesignXP = 0.1f;
    public float DevXP = .1f;
    public float FameXP = .1f;
    public int Revenue = 0;
    public int Bugs = 0;
    public bool Completed = false;

    public PlayerProjectStrategy Strategies;

    public string Description = "";
    public string Name = "";
    public int ID = -1;

    public PlayerProjectStats(PlayerProjectStrategy strategy)
    {
        this.Name = strategy.ProjectTitle;
        this.Description = strategy.GenerateDescription();
    }

    public MarketingStrategy GetMarketingStrategy() { return this.Strategies.Marketing; }
    public EmployeePolicy GetEmployeePolicy() { return this.Strategies.Policy; }

    public void AddXP(float xp, bool design = false, bool dev = false, bool fame = false, bool revenue = false)
    {
        if (design) DesignXP += xp;
        if (dev) DevXP += xp;
        if (fame) FameXP += xp;

        if (!revenue) return;
        int gain = Mathf.FloorToInt(FameXP * xp);
    }

    public void AddBug()
    {
        Bugs++;
    }

    public void RemoveBug()
    {
        Bugs--;
    }

    public void ClearStats()
    {
        Strategies.Clear();
    }

    public void AddDescription(string descriptionToAdd)
    {
        Description += descriptionToAdd;
    }
}

[System.Serializable]
public class PlayerProjectStrategy
{
    public ProjectType Type = ProjectType.None;
    public MarketingStrategy Marketing = MarketingStrategy.None;
    public EmployeePolicy Policy = EmployeePolicy.None;
    public string GeneratedDescription = "";
    public string ProjectTitle = "";

    public PlayerProjectStrategy(string name, ProjectType type = ProjectType.None, MarketingStrategy marketing = MarketingStrategy.None, EmployeePolicy policy = EmployeePolicy.None)
    {
        this.Type = type;
        this.Marketing = marketing;
        this.Policy = policy;
        this.ProjectTitle = name;
        this.GeneratedDescription = GenerateDescription();
    }

    public void Clear()
    {
        this.Type = ProjectType.None;
        this.Marketing = MarketingStrategy.None;
        this.Policy = EmployeePolicy.None;
        this.GeneratedDescription = "";
        this.ProjectTitle = "";
    }
    public string GenerateDescription()
    {
        var sb = new System.Text.StringBuilder();

        if (Type != ProjectType.None)
        {
            switch (Type)
            {
                case ProjectType.FakeSocialMedia:
                    sb.Append("Fake social media business, ");
                    break;
                case ProjectType.MailScam:
                    sb.Append("I'll scam innocent people with invoice mails to earn money, ");
                    break;
            }
        }

        if (Marketing != MarketingStrategy.None)
        {
            switch (Marketing)
            {
                case MarketingStrategy.Pay2Win:
                    sb.Append("users must pay to use the project, ");
                    break;
                case MarketingStrategy.DataHarvesting:
                    sb.Append("harvest and sell user data, ");
                    break;
            }
        }

        if (Policy != EmployeePolicy.None)
        {
            switch (Policy)
            {
                case EmployeePolicy.UnpaidInterns:
                    sb.Append("we exploit unpaid interns for profit.");
                    break;
            }
        }

        var finalDescription = sb.ToString().Trim().TrimEnd(',');
        GeneratedDescription = string.IsNullOrEmpty(finalDescription) ? "No valid strategy selected." : finalDescription;

        return GeneratedDescription;
    }


}
