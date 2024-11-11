using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actions = new List<PlayerAction>();

    private void OnMouseDown()
    {
        PlayerProjectUI projectUI = PlayerProjectUI.Instance;
        CanvasManager canvas = CanvasManager.Instance;

        canvas.CreateActionBar();

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
                    canvas.CreateActionButton("Fix Bugs").onClick.AddListener(() => projectUI.ToggleProjectContainer(true));
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
