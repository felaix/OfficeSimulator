using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class PlayerProjectManager : MonoBehaviour
{
    public static PlayerProjectManager Instance { get; set; }

    public List<PlayerProject> createdProjects = new List<PlayerProject>();
    public GameObject playerProjectPrefab;
    public PlayerProjectStrategy selectedStrategy;

    private Stats _playerStats;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_playerStats == null)
        {
            _playerStats = PlayerInventory.Instance.PlayerStats;
        }
    }

    public Stats GetPlayerStats() { return _playerStats; }

    public void UpdatePlayerProjectStrategy(PlayerProjectStrategy selection)
    {
        selectedStrategy = selection;
    }

    public void UpdatePlayerProjectType(ProjectType type)
    {
        selectedStrategy.Type = type;
        Debug.Log("strategy: " +  type);    
    }

    public void UpdatePlayerProjectMarketingStrategy(MarketingStrategy marketingStrategy)
    {
        selectedStrategy.Marketing = marketingStrategy;
        Debug.Log("strategy: " + marketingStrategy);

    }

    public void UpdatePlayerProjectEmployeePolicy(EmployeePolicy policy)
    {
        selectedStrategy.Policy = policy;
    }

    public void CreateNewPlayerProject(Transform ui)
    {
        if (ui == null) return;

        if (selectedStrategy.Type == ProjectType.None)
        {
            Debug.LogError("Error: Player selected no project type");
            return;
        }

        if (selectedStrategy.Marketing == MarketingStrategy.None)
        {
            Debug.Log("Warning: player selected no marketing strategy");
        }

        if (selectedStrategy.Policy == EmployeePolicy.None)
        {
            Debug.Log("Warning: player selected no employee policy");
        }

        Debug.Log($"Creating new Project in {ui}..");

        GameObject projectObject = Instantiate(playerProjectPrefab, ui);
        PlayerProject project = projectObject.GetComponent<PlayerProject>();
        project.Init(createdProjects.Count, selectedStrategy, _playerStats, Random.Range(2, 5));

        AddNewProject(project);
    }

    private void AddNewProject(PlayerProject project) => createdProjects.Add(project);

    public PlayerProject GetPlayerProjectByID(int id)
    {
        if (id == -1) { Debug.Log("Error Code -1"); return null; }
        return createdProjects[id];
    }

}
