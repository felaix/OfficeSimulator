using System.Collections.Generic;
using UnityEngine;

public class PlayerProject : MonoBehaviour
{
    private const float MONTHLY_PROGRESS_MULTIPLIER = 2.5f;
    private const float DEFAULT_BUG_REPAIR_TIME = 10f;

    private PlayerProjectStats playerProjectStats;
    public PlayerProjectStrategy strategy;

    [SerializeField] private ProjectItemUI UI;
    [SerializeField] private Sprite _iconCompleted;
    public int ID;

    public List<Bug> Bugs = new();

    [Header("Time")]
    public float Timer = 100f;

    private Stats playerStats;
    private float progressTime;
    private bool isCreating = true;
    private bool isToggled = false;
    private int maxBugs = 5;

    private void Start()
    {
        playerProjectStats ??= new PlayerProjectStats(strategy);
        playerStats ??= PlayerProjectManager.Instance.GetPlayerStats();

        UI.ToggleButton.onClick.AddListener(() => TogglePlayerProjectStats());
        GameClock.OnNewMonth += GameClock_OnNewMonth;

        if (isCreating)
            Debug.Log($"Creating Project ... {progressTime}");
    }

    public void Init(int id, PlayerProjectStrategy strategies, Stats playerStats = null, int maxBugs = 5)
    {
        this.ID = id;
        this.playerStats = PlayerProjectManager.Instance.GetPlayerStats();
        this.maxBugs = maxBugs;
        this.UI.SetTitle(strategies.ProjectTitle);
        this.UI.SetDescription(strategies.GenerateDescription());

        strategy.Clear();
        strategy = strategies;
        playerProjectStats = new PlayerProjectStats(strategy);
    }

    public void UpdatePlayerProjectUI() => PlayerProjectUI.Instance.SetPlayerProjectUIProgress(playerProjectStats, UI, progressTime);

    public void TogglePlayerProjectStats()
    {
        UI.projectRoot.SetActive(!isToggled);
        isToggled = !isToggled;
    }

    private void GameClock_OnNewMonth(int year, int month)
    {
        if (!isCreating || playerStats == null || playerProjectStats == null || playerProjectStats.Completed) return;

        // Fortschritt
        progressTime += playerStats.Speed * MONTHLY_PROGRESS_MULTIPLIER;
        float progress = Mathf.Clamp01(progressTime / Timer);

        UpdatePlayerProjectUI();

        if (progress >= 1f)
        {
            FinishPlayerProject();
            playerProjectStats.Completed = true;
            isCreating = false;
        }

        TryAddBug();

        AddAllXP(month);
    }

    private void TryAddBug()
    {
        if (playerProjectStats.Bugs >= maxBugs) return;

        var bug = new Bug { Project = this, TimeToRepair = DEFAULT_BUG_REPAIR_TIME, Type = BugType.CRITICAL };
        Bugs.Add(bug);
        playerProjectStats.AddBug();
    }

    private void AddAllXP(int month)
    {
        playerProjectStats.AddXP(playerStats.Design * (month / 4f), design: true);
        playerProjectStats.AddXP(playerStats.Programming * (month / 6f), dev: true);
        playerProjectStats.AddXP(playerStats.Corruption * (month / 4f), fame: true);
    }

    private void OnConfirmProjectCompleted()
    {
        float playerCorruption = PlayerInventory.Instance.PlayerStats.Corruption += .25f;
        playerProjectStats.AddXP(playerCorruption, revenue: true);
        PlayerInventory.Instance.IncreasePlayerXPByProject(playerProjectStats);
        //PlayerProjectUI.Instance.ToggleProjectContainer(true);
    }

    private void FinishPlayerProject()
    {
        Debug.Log($"{playerProjectStats.Name} completed. Bugs: {Bugs.Count}, Design Points: {playerProjectStats.DesignXP}");
        GameMessageBox.Instance.Show($"{playerProjectStats.Name} completed!", _iconCompleted, false, OnConfirmProjectCompleted);
    }

    public void FixBug(Bug bug)
    {
        if (bug == null) return;

        if (Bugs.Remove(bug))
        {
            playerProjectStats.RemoveBug();
        }
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
