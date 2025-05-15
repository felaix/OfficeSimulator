using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectManager : MonoBehaviour
{
    public static PlayerProjectManager Instance { get; set; }

    public List<PlayerProject> createdProjects = new List<PlayerProject>();

    private Stats _playerStats;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_playerStats == null)
        {
            _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        }
    }

    public Stats GetPlayerStats() { return _playerStats; }

    public void CreateNewPlayerProject()
    {
        GameObject projectObject = new GameObject("Generated Project Object");
        PlayerProject newProject = projectObject.AddComponent<PlayerProject>();
        AddNewProject(newProject);
    }

    private void AddNewProject(PlayerProject project)
    {
        createdProjects.Add(project);
        project.GameID = createdProjects.Count - 1;
    }

    public PlayerProject GetPlayerProjectByID(int id)
    {
        if (id == -1) { Debug.Log("Error Code -1"); return null; }
        return createdProjects[id];
    }

}
