using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProjectUI : MonoBehaviour
{
    public static PlayerProjectUI Instance { get; private set; }

    public List<ProjectItemUI> ProjectItems = new List<ProjectItemUI>();

    [SerializeField] private Transform _createProjectWindow;

    [SerializeField] private Transform _projectContainer;
    [SerializeField] private GameObject _projectPrefab;

    [SerializeField] private GameObject _bugPrefab;
    [SerializeField] private Transform _bugContainer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ToggleProjectContainer(false);
    }

    public void ToggleProjectContainer(bool active) { _projectContainer.gameObject.SetActive(active); }

    public void ToggleCreateProjectWindow(bool active) { _createProjectWindow.gameObject.SetActive(active); }


    public void CreateBugWindow(PlayerProject project)
    {
        _bugContainer.gameObject.SetActive(true);

        if (project.GameBugs > 0)
        {
            foreach (var bug in project.Bugs)
            {
                ProjectBug bugUI = Instantiate(_bugPrefab, _bugContainer).GetComponent<ProjectBug>();
                bugUI.InitializeBugUI(bug);
            }
        }

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