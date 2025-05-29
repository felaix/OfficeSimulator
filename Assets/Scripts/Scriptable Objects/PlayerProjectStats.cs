using System.Text;
using UnityEngine;

[System.Serializable]
public class PlayerProjectStats
{
    [Header("Punkte")]
    public float DesignXP = 0f;
    public float DevXP = 0f;
    public float FameXP = 0f;
    public int Revenue = 0;
    public int Bugs = 0;
    public bool Completed = false;

    public PlayerProjectStrategy Strategy { get; private set; }

    public string Description = "";
    public string Name { get; private set; } = "";
    public int ID = -1;

    public PlayerProjectStats(PlayerProjectStrategy strategy, int id = -1)
    {
        Strategy = strategy;
        Name = strategy.ProjectTitle;
        Description = strategy.GenerateDescription();
        ID = id;
    }

    public MarketingStrategy GetMarketingStrategy() => Strategy.Marketing;
    public EmployeePolicy GetEmployeePolicy() => Strategy.Policy;

    /// <summary>
    /// Fügt XP gemäß flags hinzu. Wenn revenue=true, berechnet und addiert auch Revenue.
    /// </summary>
    public void AddXP(float xp, bool design = false, bool dev = false, bool fame = false, bool revenue = false)
    {
        if (design) DesignXP += xp;
        if (dev) DevXP += xp;
        if (fame) FameXP += xp;

        if (revenue)
        {
            // Beispiel: Revenue = Floor(FameXP * xp)
            int gain = Mathf.FloorToInt(FameXP * xp);
            Revenue += Mathf.Max(gain, 0);
        }
    }

    public void AddBug()
    {
        Bugs++;
    }

    public void RemoveBug()
    {
        if (Bugs > 0) Bugs--;
    }

    /// <summary>
    /// Setzt alle Stats zurück, entfernt Strategy-Referenz und leeren Name/Description.
    /// </summary>
    public void ClearStats()
    {
        DesignXP = DevXP = FameXP = 0f;
        Revenue = Bugs = 0;
        Completed = false;
        Strategy = null;
        Name = "";
        Description = "";
        ID = -1;
    }

    public void AddDescription(string additional)
    {
        if (string.IsNullOrEmpty(additional)) return;
        Description = string.Concat(Description, " ", additional).Trim();
    }
}

[System.Serializable]
public class PlayerProjectStrategy
{
    public ProjectType Type = ProjectType.None;
    public MarketingStrategy Marketing = MarketingStrategy.None;
    public EmployeePolicy Policy = EmployeePolicy.None;

    public string GeneratedDescription { get; private set; } = "";
    public string ProjectTitle = "";

    public PlayerProjectStrategy() { }

    public PlayerProjectStrategy(string name, ProjectType type = ProjectType.None,
                                 MarketingStrategy marketing = MarketingStrategy.None,
                                 EmployeePolicy policy = EmployeePolicy.None)
    {
        ProjectTitle = name;
        Type = type;
        Marketing = marketing;
        Policy = policy;
        GeneratedDescription = GenerateDescription();
    }

    /// <summary>
    /// Setzt alle Felder auf None bzw. leeren Text zurück.
    /// </summary>
    public void Clear()
    {
        Type = ProjectType.None;
        Marketing = MarketingStrategy.None;
        Policy = EmployeePolicy.None;
        GeneratedDescription = "";
        ProjectTitle = "";
    }

    /// <summary>
    /// Erzeugt eine Beschreibung basierend auf den aktuell gesetzten Enum-Feldern.
    /// </summary>
    public string GenerateDescription()
    {
        var sb = new StringBuilder();

        // Projekttyp
        switch (Type)
        {
            case ProjectType.TrashGame:
                sb.Append("Trash Game: Ein lieblos produziertes Game mit minimalem Aufwand.");
                break;
            case ProjectType.FakeSocialMedia:
                sb.Append("Fake Social Media: Sammle und verkaufe Nutzerdaten hinter falschem Versprechen.");
                break;
            case ProjectType.FakeOnlineShop:
                sb.Append("Fake Online Shop: Täusche Kunden mit nicht existierenden Produkten.");
                break;
            case ProjectType.MailScam:
                sb.Append("Mail Scam: Versende betrügerische E-Mails, um Geld abzugreifen.");
                break;
        }

        // Marketing-Strategie
        switch (Marketing)
        {
            case MarketingStrategy.BlockchainIntegration:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Blockchain Integration: Leere Buzzwords, um Investoren zu ködern.");
                break;
            case MarketingStrategy.NFT:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("NFT: Verkaufe wertlose Pixelbilder als vermeintliche Sammlerstücke.");
                break;
            case MarketingStrategy.Pay2Win:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Pay2Win: Spieler müssen zahlen, um im Spiel voranzukommen.");
                break;
            case MarketingStrategy.DataHarvesting:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Data Harvesting: Sammle Nutzerdaten, um sie gewinnbringend weiterzuverkaufen.");
                break;
            case MarketingStrategy.ReferralPyramidSystem:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Referral Pyramid System: Lock Kunden mit dem Versprechen hoher Gewinne.");
                break;
            case MarketingStrategy.FakeReviews:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Fake Reviews: Bestich Influencer, um glänzende Bewertungen zu simulieren.");
                break;
        }

        // Mitarbeiter-Policy
        switch (Policy)
        {
            case EmployeePolicy.CheapFreelancer:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Cheap Freelancer: Arbeite mit Billigkräften, um Personalkosten zu drücken.");
                break;
            case EmployeePolicy.UnpaidInterns:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Unpaid Interns: Nutze unbezahlte Praktikanten für horizontale Profite.");
                break;
            case EmployeePolicy.CrunchTime:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Crunch Time: Unterdrücke Work-Life-Balance für maximale Effizienz.");
                break;
            case EmployeePolicy.FakeWellnessProgram:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Fake Wellness Program: Simuliere Mitarbeiterfürsorge ohne echte Leistungen.");
                break;
            case EmployeePolicy.CultureOfFear:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Culture of Fear: Einschüchterung statt echter Motivation.");
                break;
            case EmployeePolicy.PyramidReferrals:
                if (sb.Length > 0) sb.Append(" ");
                sb.Append("Pyramid Referrals: Belohne Mitarbeiter dafür, neue Mitarbeiter anzuwerben.");
                break;
        }

        var result = sb.ToString().Trim();
        if (string.IsNullOrEmpty(result))
            result = "No valid strategy selected.";

        GeneratedDescription = result;
        return GeneratedDescription;
    }
}
