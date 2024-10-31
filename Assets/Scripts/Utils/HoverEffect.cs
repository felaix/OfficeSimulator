using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HoverEffect : MonoBehaviour
{
    private Renderer objectRenderer;
    private Material originalMaterial;
    public Material outlineMaterial;
    public float hoverHeight = 0.2f; // Amount to move up on hover

    private Vector3 originalPosition;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        originalPosition = transform.position;
    }

    private void OnMouseEnter()
    {
        // Apply outline material
        if (outlineMaterial != null)
        {
            objectRenderer.material = outlineMaterial;
        }

        // Move the object up slightly
        transform.position = originalPosition + new Vector3(0, hoverHeight, 0);
    }

    private void OnMouseExit()
    {
        // Revert to the original material
        objectRenderer.material = originalMaterial;

        // Move the object back to its original position
        transform.position = originalPosition;
    }
}
