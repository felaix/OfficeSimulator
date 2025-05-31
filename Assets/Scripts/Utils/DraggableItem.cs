using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableItem : MonoBehaviour
{
    public LayerMask dropLayer;

    public Material outlineMaterial;
    private Material originalMaterial;
    private Renderer rend;

    private Camera mainCamera;
    private Vector3 dragOffset;

    private bool isDragging = false;
    private bool isHovered = false;
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
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            rend = GetComponentInChildren<Renderer>();
        }

        if (rend != null)
        {
            originalMaterial = rend.material;
        }
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

    private void SetOutline(bool active)
    {
        if (rend == null || originalMaterial == null || outlineMaterial == null) return;

        rend.material = active ? outlineMaterial : originalMaterial;
        isHovered = active;
    }

    private void Update()
    {

        Vector2 pointerPos = input.Game.Point.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(pointerPos);


        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            bool isThis = hit.transform == transform;
            if (isThis && !isHovered)
            {
                SetOutline(true);

            }
            else if (!isThis && isHovered)
            {
                SetOutline(false);
            }
        }
        else if (isHovered)
        {
            SetOutline(false);
        }

        if (!isDragging) return;
        else
        {
            Vector3 target = hit.point + Vector3.up + dragOffset;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 15f);
        }
    }
}
