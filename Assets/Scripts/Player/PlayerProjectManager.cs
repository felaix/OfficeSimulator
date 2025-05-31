using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class PlayerProjectManager : MonoBehaviour
{
    public static PlayerProjectManager Instance { get; private set; }

    [Header("Project Setup")]
    [SerializeField] private GameObject playerProjectPrefab;
    [SerializeField] private Transform defaultParentContainer;

    [Header("Project Names")]
    [SerializeField]
    private string[] defaultProjectNames = {
        "Exploitation Simulator",
        "Virtual Grind",
        "Soul Extraction",
        "Wage Slave Tycoon",
        "Profit Over People",
        "Burnout Factory",
        "Corporate Dystopia",
        "The Rat Race"
    };

    [Header("Debug")]
    [SerializeField] private bool verboseLogging = false;

    // Current strategy selected in UI
    [HideInInspector] public PlayerProjectStrategy selectedStrategy;

    // All created projects
    private List<PlayerProject> _createdProjects = new List<PlayerProject>();
    private Stats _playerStats;

    public IReadOnlyList<PlayerProject> CreatedProjects => _createdProjects;

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
        if (_playerStats == null && verboseLogging)
        {
            Debug.LogWarning("PlayerStats not found - projects will have no stat effects");
        }
    }

    public void CreateNewPlayerProject()
    {
        if (defaultParentContainer == null)
        {
            defaultParentContainer = transform;
            if (verboseLogging) Debug.Log("Using manager transform as default container");
        }

        CreateProjectAt(defaultParentContainer);
    }

    public void CreateProjectAt(Transform parentContainer)
    {
        // Validate inputs
        if (!ValidateCreationParameters(parentContainer))
            return;

        // Ensure we have a title
        if (string.IsNullOrWhiteSpace(selectedStrategy.ProjectTitle))
        {
            selectedStrategy.ProjectTitle = GenerateProjectName();
            if (verboseLogging) Debug.Log($"Generated project name: {selectedStrategy.ProjectTitle}");
        }

        // Create the project object
        var newProject = InstantiateProject(parentContainer);
        if (newProject == null) return;

        // Initialize and track
        newProject.Initialize(
            id: _createdProjects.Count,
            strategy: selectedStrategy,
            playerStats: _playerStats
        );

        _createdProjects.Add(newProject);
        PostCreationCleanup();

        if (verboseLogging) Debug.Log($"Created new project: {selectedStrategy.ProjectTitle}");
    }

    private bool ValidateCreationParameters(Transform parentContainer)
    {
        if (parentContainer == null)
        {
            Debug.LogError("Cannot create project - no parent container specified");
            return false;
        }

        if (selectedStrategy == null)
        {
            Debug.LogError("Cannot create project - no strategy selected");
            return false;
        }

        if (selectedStrategy.Type == ProjectType.None)
        {
            Debug.LogError("Cannot create project - no project type selected");
            return false;
        }

        return true;
    }

    private string GenerateProjectName()
    {
        return defaultProjectNames[Random.Range(0, defaultProjectNames.Length)];
    }

    private PlayerProject InstantiateProject(Transform parent)
    {
        var projectGO = Instantiate(playerProjectPrefab, parent);
        var project = projectGO.GetComponent<PlayerProject>();

        if (project == null)
        {
            Debug.LogError("Project prefab missing PlayerProject component!");
            Destroy(projectGO);
            return null;
        }

        return project;
    }

    private void PostCreationCleanup()
    {
        // Clear UI selections
        if (PlayerProjectUI.Instance != null)
        {
            PlayerProjectUI.Instance.ClearAllSelections();
            PlayerProjectUI.Instance.ToggleCreateWindow(false);
        }

        // Reset for next creation
        selectedStrategy = null;
    }

    public PlayerProject GetProjectByID(int id)
    {
        if (id < 0 || id >= _createdProjects.Count)
        {
            Debug.LogError($"Invalid project ID: {id}");
            return null;
        }
        return _createdProjects[id];
    }
}