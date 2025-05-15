using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableItem : MonoBehaviour
{
    public LayerMask dropLayer;
    private Camera mainCamera;
    private Vector3 dragOffset;

    private bool isDragging = false;
    private PlayerInputActions input;

    private void Awake()
    {
        input = new PlayerInputActions();
        input.Enable();

        input.Game.Click.started += OnClickStarted;
        input.Game.Click.canceled += OnClickCanceled;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnDestroy()
    {
        input.Disable();
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        Vector2 pointerPos = input.Game.Point.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(pointerPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f) && hit.transform == transform)
        {
            isDragging = true;
            dragOffset = transform.position - hit.point;
        }
    }


    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        isDragging = false;
    }

    private void Update()
    {
        if (!isDragging) return;

        Vector2 pointerPos = input.Game.Point.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(pointerPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, dropLayer))
        {
            Vector3 target = hit.point + Vector3.up + dragOffset;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 15f);
        }
    }
}
