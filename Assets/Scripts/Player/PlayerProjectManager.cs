using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class PlayerProjectManager : MonoBehaviour
{
    public static PlayerProjectManager Instance { get; private set; }

    [Header("Projekt-Prefab & Strategy")]
    [SerializeField] private GameObject playerProjectPrefab;

    // Wird dynamisch von den Buttons gefüllt
    [HideInInspector] public PlayerProjectStrategy selectedStrategy;

    [HideInInspector] public List<PlayerProject> createdProjects = new List<PlayerProject>();
    private Stats _playerStats;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _playerStats = PlayerInventory.Instance?.PlayerStats;
        if (_playerStats == null)
            Debug.LogError("PlayerStats nicht gefunden!");
    }

    /// <summary>
    /// Gibt PlayerStats zurück (für Projekt-Init).
    /// </summary>
    public Stats GetPlayerStats() => _playerStats;

    /// <summary>
    /// Erstellt ein neues Projekt-GameObject unter parentContainer,
    /// basierend auf der bereits in selectedStrategy gesetzten Strategy.
    /// </summary>
    public void CreateNewPlayerProject(Transform parentContainer)
    {
        if (parentContainer == null)
        {
            Debug.LogError("UI-Container ist null!");
            return;
        }

        if (selectedStrategy == null)
        {
            Debug.LogError("selectedStrategy ist null!");
            return;
        }

        // Diese Prüfungen machen die ConfirmCreateProjectButton bereits, 
        // aber zur Sicherheit nochmal:
        if (selectedStrategy.Type == ProjectType.None)
        {
            Debug.LogError("Kein Projekttyp gewählt!");
            return;
        }
        if (selectedStrategy.Marketing == MarketingStrategy.None)
            Debug.LogWarning("Keine Marketing-Strategie gewählt!");
        if (selectedStrategy.Policy == EmployeePolicy.None)
            Debug.LogWarning("Keine Mitarbeiter-Policy gewählt!");

        // Title & Description endgültig setzen (kann überschrieben werden, wenn Spieler noch editiert)
        selectedStrategy.ProjectTitle = selectedStrategy.ProjectTitle == ""
            ? "New Project"
            : selectedStrategy.ProjectTitle;

        selectedStrategy.GenerateDescription();

        // Instantiate Prefab
        var go = Instantiate(playerProjectPrefab, parentContainer);
        var proj = go.GetComponent<PlayerProject>();
        if (proj == null)
        {
            Debug.LogError("Prefab hat kein PlayerProject-Skript!");
            Destroy(go);
            return;
        }

        // Init mit ID, Strategy und PlayerStats
        proj.Initialize(createdProjects.Count, selectedStrategy, _playerStats, Random.Range(2, 5));
        createdProjects.Add(proj);
    }

    /// <summary>
    /// Hilfsmethode, um anhand einer ID das Projekt zu bekommen.
    /// </summary>
    public PlayerProject GetPlayerProjectByID(int id)
    {
        if (id < 0 || id >= createdProjects.Count)
        {
            Debug.LogError($"Ungültige Projekt-ID: {id}");
            return null;
        }
        return createdProjects[id];
    }
}
