using TMPro;
using UnityEngine;

public class PlayerProjectUI : MonoBehaviour
{
    public static PlayerProjectUI Instance { get; private set; }

    [Header("Essential")]
    [SerializeField] private Transform _createProjectWindow;
    [SerializeField] private Transform _projectContainer;
    [SerializeField] private GameObject _projectPrefab;

    [Header("Bugs")]
    [SerializeField] private GameObject _bugPrefab;
    [SerializeField] private Transform _bugContainer;

    [Header("Description & Title")]
    [SerializeField] private TMP_Text _descriptionTMP;
    [SerializeField] private TMP_InputField _textInputField;

    private GameObject selectedProjectTypeIcon;
    private GameObject selectedMarketingTypeIcon;
    private GameObject selectedPolicyTypeIcon;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ToggleProjectContainer(false);
    }

    private void OnDisable()
    {
        ClearIcons();
    }

    public void ClearIcons()
    {
        selectedProjectTypeIcon = null;
        selectedMarketingTypeIcon = null;
        selectedPolicyTypeIcon = null;
    }

    public void SelectProjectType(GameObject selectedIcon)
    {
        if (selectedProjectTypeIcon != null) selectedProjectTypeIcon.SetActive(false);
        selectedProjectTypeIcon = selectedIcon;
    }

    public void SelectMarketingType(GameObject selectedIcon)
    {
        if (selectedMarketingTypeIcon != null) selectedMarketingTypeIcon.SetActive(false);
        selectedMarketingTypeIcon = selectedIcon;
    }

    public void SelectPolicyType(GameObject selectedIcon)
    {
        if (selectedPolicyTypeIcon != null) selectedPolicyTypeIcon.SetActive(false);
        selectedPolicyTypeIcon = selectedIcon;
    }

    public void ToggleProjectContainer(bool active) { _projectContainer.gameObject.SetActive(active); }

    public void ToggleCreateProjectWindow(bool active) { _createProjectWindow.gameObject.SetActive(active); }

    public void CreateBugWindow(PlayerProject project)
    {
        if (_bugContainer == null || _bugPrefab == null)
        {
            Debug.LogError("Bug container or prefab not assigned");
            return;
        }

        if (project == null)
        {
            Debug.LogError("Project is null");
            return;
        }

        _bugContainer.gameObject.SetActive(true);

        if (project.Bugs.Count >= 1)
        {
            foreach (var bug in project.Bugs)
            {
                ProjectBug bugUI = Instantiate(_bugPrefab, _bugContainer).GetComponent<ProjectBug>();
                bugUI.InitializeBugUI(bug);
            }
        }

    }

    //public void CreatePlayerProject() => PlayerProjectManager.Instance.CreateNewPlayerProject(_projectContainer);

    public void SetPlayerProjectUIProgress(PlayerProjectStats playerProjectStats, ProjectItemUI ui, float progress)
    {
        if (playerProjectStats == null || ui == null) return;

        if (progress >= 101)
        {
            ui.SetProgress("completed");
        }
        else
        {
            ui.SetProgress(progress.ToString() + "%");
        }

        ui.SetDesign(playerProjectStats.DesignXP.ToString());
        ui.SetBugs(playerProjectStats.Bugs.ToString());
        ui.SetProgramming(playerProjectStats.DevXP.ToString());
        ui.SetName(playerProjectStats.Name);
        ui.SetDescription(playerProjectStats.Description);
    }
}