using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private List<PlayerAction> actions = new();
    [SerializeField] private GameObject bugPrefab;         // Bug-Prefab zuweisen
    [SerializeField] private Transform bugContainer;       // Bug-Container zuweisen

    private PlayerInputActions input;
    private Camera mainCam;
    private DraggableItem draggableItem;

    private void Awake()
    {
        input = new PlayerInputActions();
        input.Game.DoublePress.performed += OnSpawnRequested;
        input.Game.RightClick.performed += OnSpawnRequested;
        input.Enable();

        mainCam = Camera.main;
        draggableItem = GetComponent<DraggableItem>();
    }

    private void OnDestroy()
    {
        input.Game.DoublePress.performed -= OnSpawnRequested;
        input.Game.RightClick.performed -= OnSpawnRequested;
        input.Disable();
    }

    private void OnSpawnRequested(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = input.Game.Point.ReadValue<Vector2>();
        Ray ray = mainCam.ScreenPointToRay(screenPos);

        if (!Physics.Raycast(ray, out RaycastHit hit) || hit.transform != transform)
            return;

        ShowActions();
    }

    private void ShowActions()
    {
        var canvas = CanvasManager.Instance;
        var projectUI = PlayerProjectUI.Instance;
        var projectManager = PlayerProjectManager.Instance;

        if (canvas == null || projectUI == null || projectManager == null)
        {
            Debug.LogError("ActionBar: Missing CanvasManager, PlayerProjectUI or PlayerProjectManager instance.");
            return;
        }

        // Wenn dieses Objekt ein PlayerProject ist, holen wir uns die ID
        int projectID = TryGetComponent<PlayerProject>(out var proj) ? proj.ID : -1;
        canvas.CreateActionBar();

        Computer com = GetComponent<Computer>();
        if (com != null) { com.ToggleProgressUI(); }

        foreach (var action in actions)
        {
            switch (action)
            {
                case PlayerAction.SHOW_BUTTON_PROJECT:
                    CreateButton(canvas, "Show Projects", () => projectUI.ToggleProjectList(true));
                    break;

                case PlayerAction.SHOW_BUTTON_SOCIAL_MEDIA:
                    CreateButton(canvas, "Check Reviews", () => projectUI.ToggleProjectList(true));
                    break;

                case PlayerAction.SHOW_BUTTON_FIX_BUGS:
                    if (projectID < 0)
                    {
                        Debug.LogError("Kein Projekt hier zum Bugfixen.");
                        break;
                    }
                    CreateButton(canvas, "Fix Bugs", () =>
                    {
                        Debug.Log("nix zum fixen.");
                    });
                    break;

                case PlayerAction.SHOW_BUTTON_NEW_PROJECT:
                    CreateButton(canvas, "Create New Project", () => projectUI.ToggleCreateWindow(true));
                    break;

                case PlayerAction.SHOW_BUTTON_SLEEP:
                    CreateButton(canvas, "Sleep", () =>
                        PlayerInventory.Instance.PlayerStats.ModifySleep(50f, true)
                    );
                    break;

                case PlayerAction.SHOW_BUTTON_RELAX:
                    CreateButton(canvas, "Relax", () =>
                        PlayerInventory.Instance.PlayerStats.ModifySleep(12.5f, true)
                    );
                    break;

                case PlayerAction.SHOW_BUTTON_COOK:
                    CreateButton(canvas, "Cook", () => Cook());
                    break;

                case PlayerAction.NONE:
                default:
                    break;
            }
        }
    }

    private void CreateButton(CanvasManager canvas, string label, Action onClick)
    {
        var btn = canvas.CreateActionButton(label);
        if (btn != null)
            btn.onClick.AddListener(() => onClick());
    }

    private void Cook()
    {
        // Falls in Zukunft Implementierung nötig ist
        Debug.LogWarning("Cook() noch nicht implementiert.");
    }
}
