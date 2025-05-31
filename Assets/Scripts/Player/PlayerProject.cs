// PlayerProject.cs
using UnityEngine;

public class PlayerProject : MonoBehaviour
{
    private const float MONTHLY_PROGRESS_MULTIPLIER = 2.5f;
    private const float DEFAULT_BUG_REPAIR_TIME = 10f;

    [Header("UI & Icon")]
    [SerializeField] private ProjectItemUI UI;
    [SerializeField] private Sprite iconCompleted;

    [HideInInspector] public int ID;
    [HideInInspector] public PlayerProjectStrategy Strategy;
    [HideInInspector] public PlayerProjectStats Stats;

    [HideInInspector] public float MaxTime;
    [HideInInspector] public bool IsCreating = true;

    private Stats _playerStats;
    private float _elapsedTime;
    private bool _isToggled;
    private int _maxBugs;
    private readonly System.Collections.Generic.List<Bug> _bugs = new();

    // Events unregistern
    private void OnDestroy()
    {
        GameClock.OnNewMonth -= OnNewMonth;
        if (UI?.ToggleButton != null)
            UI.ToggleButton.onClick.RemoveListener(ToggleUI);
    }

    public void Initialize(int id, PlayerProjectStrategy strategy, Stats playerStats, int maxBugs = 5)
    {
        ID = id;
        Strategy = strategy;
        _playerStats = playerStats;
        _maxBugs = maxBugs;
        MaxTime = Random.Range(50f, 100f); // zum Beispiel

        // Stats-Objekt aufbauen
        Stats = new PlayerProjectStats(Strategy);
        UI.SetTitle(Strategy.ProjectTitle);
        UI.SetDescription(Strategy.GeneratedDescription);

        // Event-Listener
        UI.ToggleButton.onClick.AddListener(ToggleUI);
        GameClock.OnNewMonth += OnNewMonth;
    }

    private void ToggleUI()
    {
        if (UI?.projectRoot != null)
        {
            _isToggled = !_isToggled;
            UI.projectRoot.SetActive(_isToggled);
        }
    }

    private void OnNewMonth(int year, int month)
    {
        if (!IsCreating || _playerStats == null || Stats.Completed)
            return;

        // Projekt-Fortschritt
        _elapsedTime += _playerStats.Speed;
        float progressNormalized = Mathf.Clamp01(_elapsedTime / MaxTime);
        UpdateUI(progressNormalized);

        if (progressNormalized >= 1f)
        {
            IsCreating = false;
            Stats.Completed = true;
            FinishProject();
        }

        TryAddBug();
        AwardMonthlyXP(month);
    }

    private void UpdateUI(float normalizedProgress)
    {
        if (UI == null) return;
        float percent = normalizedProgress * 100f;
        UI.SetProgress(percent >= 100f ? "completed" : $"{percent:0}%");
        UI.SetDesign(Stats.DesignXP.ToString());
        UI.SetBugs(Stats.Bugs.ToString());
        UI.SetProgramming(Stats.DevXP.ToString());
        UI.SetName(Stats.Name);
        UI.SetDescription(Stats.Description);
    }

    private void TryAddBug()
    {
        if (Stats.Bugs >= _maxBugs) return;

        var bug = new Bug { Project = this, TimeToRepair = DEFAULT_BUG_REPAIR_TIME, Type = BugType.CRITICAL };
        _bugs.Add(bug);
        Stats.AddBug();
    }

    private void AwardMonthlyXP(int month)
    {
        Stats.AddXP(_playerStats.Design * (month / 4f), design: true);
        Stats.AddXP(_playerStats.Programming * (month / 6f), dev: true);
        Stats.AddXP(_playerStats.Corruption * (month / 4f), fame: true);
    }

    private void FinishProject()
    {
        Debug.Log($"Projekt \"{Stats.Name}\" fertig. Bugs: {_bugs.Count}, DesignXP: {Stats.DesignXP}");
        GameMessageBox.Instance.Show(
            $"{Stats.Name} abgeschlossen!",
            iconCompleted,
            false,
            OnConfirmComplete
        );
    }

    private void OnConfirmComplete()
    {
        // Wenn PlayerCorruption erhöht wird, direkt an Inventory weitergeben
        float newCorruption = PlayerInventory.Instance.PlayerStats.Corruption + 0.25f;
        PlayerInventory.Instance.PlayerStats.Corruption = newCorruption;
        Stats.AddXP(newCorruption, revenue: true);
        PlayerInventory.Instance.IncreasePlayerXPByProject(Stats);
    }

    public void FixBug(Bug bug)
    {
        if (bug == null || !_bugs.Remove(bug)) return;
        Stats.RemoveBug();
    }
}

public enum ProjectType
{
    TrashGame,
    FakeSocialMedia,
    FakeOnlineShop,
    MailScam,
    None
}

public class Bug
{
    public BugType Type;
    public float TimeToRepair;
    public PlayerProject Project;
}
