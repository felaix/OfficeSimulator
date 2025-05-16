using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actions = new List<PlayerAction>();
    private DraggableItem item;
    private PlayerInputActions input;
    private Camera cam;

    private void Awake()
    {
        input = new PlayerInputActions();
        //input.Game.Hold.performed += OnHoldPerformed;
        input.Game.DoublePress.performed += OnDoublePressPerformed;
        input.Enable();

        cam = Camera.main;
    }

    private void OnDoublePressPerformed(InputAction.CallbackContext context)
    {
        Vector2 pointerPos = input.Game.Point.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(pointerPos);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.cyan, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                InitializeActionbar();
            }
        }
    }

    private void OnDestroy()
    {
        //input.Game.Hold.performed -= OnHoldPerformed;
        input.Disable();
    }

    private void Start()
    {
        item = GetComponent<DraggableItem>();
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
                    if (manager != null && projectUI != null)
                    {
                        PlayerProject targetProject = manager.GetPlayerProjectByID(projectID);
                        if (targetProject != null)
                        {
                            canvas.CreateActionButton("Fix Bugs").onClick.AddListener(() => projectUI.CreateBugWindow(targetProject));
                        }
                        else
                        {
                            Debug.LogError($"Project with ID {projectID} not found");
                        }
                    }
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
