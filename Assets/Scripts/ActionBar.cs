using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actions = new List<PlayerAction>();

    private void OnMouseDown()
    {
        InitializeActionbar();
    }

    public void InitializeActionbar()
    {
        PlayerProjectUI projectUI = PlayerProjectUI.Instance;
        PlayerProjectManager manager = PlayerProjectManager.Instance;
        CanvasManager canvas = CanvasManager.Instance;

        int projectID = -1;

        if (TryGetComponent<ProjectItemUI>(out ProjectItemUI project))
        {
            projectID = project.ID;
        }

        canvas.CreateActionBar();

        Debug.Log("init action bar");

        foreach (var item in actions)
        {
            switch (item)
            {
                case PlayerAction.SHOW_BUTTON_PROJECT:
                    canvas.CreateActionButton("Show Projects").onClick.AddListener(() => projectUI.ToggleProjectContainer(true));
                    break;
                case PlayerAction.SHOW_BUTTON_SOCIAL_MEDIA:
                    canvas.CreateActionButton("Check Reviews").onClick.AddListener(() => projectUI.ToggleProjectContainer(true));
                    break;
                case PlayerAction.SHOW_BUTTON_FIX_BUGS:
                    PlayerProject targetProject = manager.GetPlayerProjectByID(projectID);
                    canvas.CreateActionButton("Fix Bugs").onClick.AddListener(() => projectUI.CreateBugWindow(targetProject));
                    break;
                case PlayerAction.SHOW_BUTTON_NEW_PROJECT:
                    canvas.CreateActionButton("Create new Project").onClick.AddListener(() => projectUI.ToggleCreateProjectWindow(true));
                    break;
                default:
                    break;
            }
        }
    }

}

public enum PlayerAction
{
    SHOW_BUTTON_PROJECT,
    SHOW_BUTTON_NEW_PROJECT,
    SHOW_BUTTON_SOCIAL_MEDIA,
    SHOW_BUTTON_FIX_BUGS,
}
