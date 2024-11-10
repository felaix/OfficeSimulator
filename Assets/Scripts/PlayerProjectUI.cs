using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProjectUI : MonoBehaviour
{
    public static PlayerProjectUI Instance { get; private set; }

    public List<ProjectItemUI> ProjectItems = new List<ProjectItemUI>();

    [SerializeField] private Transform _projectContainer;
    [SerializeField] private GameObject _projectPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _projectContainer.gameObject.SetActive(false);
    }

    public void CreatePlayerProjectUI()
    {
        ProjectItemUI project = Instantiate(_projectPrefab, _projectContainer).GetComponent<ProjectItemUI>();
        ProjectItems.Add(project);
        project.ID = ProjectItems.Count - 1;
    }

    public void SetPlayerProjectUIProgress(PlayerProject project, int progress = 0)
    {
        _projectContainer.gameObject.SetActive(true);
        
        int id = project.GameID;
        ProjectItemUI ui = GetProjectItemByID(id);


        if (progress >= 99)
        {
            ui.SetProgress("completed");
        }
        else
        {
            ui.SetProgress(progress + "%");

        }

        ui.SetDesign(project.GameDesignPoints.ToString());
        ui.SetBugs(project.GameBugs.ToString());
        ui.SetProgramming(project.GameDevPoints.ToString());

    }

    private ProjectItemUI GetProjectItemByID(int id)
    {
        return ProjectItems[id];
    }
}
