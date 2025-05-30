using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    public Transform target;
    public float rotationSpeed = 0.3f;
    public float panSpeed = 0.01f;
    public float moveSpeed = 0.1f; // New variable for WASD movement speed
    public float zoomSpeed = 0.1f;
    public float smoothTime = 0.1f;

    [Header("Zoom Limits")]
    public float minZoom = 5f;
    public float maxZoom = 20f;

    [Header("Pan Limits")]
    public Vector2 panLimitX = new Vector2(-10f, 10f);
    public Vector2 panLimitZ = new Vector2(-10f, 10f);

    private PlayerInputActions input;
    private Camera cam;

    private bool isRotating = false;
    private bool isPanning = false;

    private float currentRotation;
    private float targetRotation;
    private float rotationVelocity;

    private Vector3 targetPanOffset;
    private Vector3 panVelocity;
    private Vector2 moveInput;

    private float targetZoom;
    private float zoomVelocity;

    private void Awake()
    {
        input = new PlayerInputActions();

        input.Camera.Rotate.started += ctx => isRotating = true;
        input.Camera.Rotate.canceled += ctx => isRotating = false;

        input.Camera.Pan.started += ctx => isPanning = true;
        input.Camera.Pan.canceled += ctx => isPanning = false;

        input.Camera.Look.performed += OnLookPerformed;
        input.Camera.Zoom.performed += OnZoomPerformed;

        input.Camera.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Camera.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Camera.Enable();
    }

    private void Start()
    {
        cam = Camera.main;
        currentRotation = transform.eulerAngles.y;
        targetRotation = currentRotation;
        targetPanOffset = transform.position - target.position;
        targetZoom = cam.orthographicSize;

        StartCoroutine(CameraLoop());
    }

    private void OnDestroy()
    {
        input.Camera.Look.performed -= OnLookPerformed;
        input.Camera.Zoom.performed -= OnZoomPerformed;
        input.Camera.Disable();
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            delta *= 0.5f;

        if (isRotating)
        {
            targetRotation += delta.x * rotationSpeed;
            targetRotation = Mathf.Repeat(targetRotation, 360f);
            SmoothRotate();
        }
        else if (isPanning)
        {
            Vector3 right = cam.transform.right;
            Vector3 up = cam.transform.up;
            Vector3 move = (-right * delta.x + -up * delta.y) * panSpeed;
            targetPanOffset += move;
            SmoothPan();
        }
    }

    private void OnZoomPerformed(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<Vector2>().y;
        targetZoom = Mathf.Clamp(targetZoom - scrollValue * zoomSpeed, minZoom, maxZoom);
        SmoothZoom();
    }

    private void SmoothRotate()
    {
        float newY = Mathf.SmoothDampAngle(currentRotation, targetRotation, ref rotationVelocity, smoothTime);
        float deltaAngle = Mathf.DeltaAngle(currentRotation, newY);

        currentRotation = newY;
        transform.RotateAround(target.position, Vector3.up, deltaAngle);

        // Update targetPanOffset after rotation
        targetPanOffset = transform.position - target.position;
    }

    private void SmoothPan()
    {
        Vector3 targetPosition = target.position + targetPanOffset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, panLimitX.x, panLimitX.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, panLimitZ.x, panLimitZ.y);

        transform.position = targetPosition;
    }



    private void SmoothZoom()
    {
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);
    }

    private IEnumerator CameraLoop()
    {
        while (true)
        {
            if (moveInput != Vector2.zero)
            {
                Vector3 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
                Vector3 right = cam.transform.right;

                Vector3 moveDir = (right * moveInput.x + forward * moveInput.y) * moveSpeed;
                targetPanOffset += moveDir;
            }

            if (isRotating)
                SmoothRotate();

            if (isPanning)
            {
                // Pan-Eingabe verändert targetPanOffset bereits
            }

            SmoothPan(); // Immer aufrufen
            SmoothZoom();

            yield return null;
        }
    }

}