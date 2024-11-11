using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public bool isDragging = false;
    public LayerMask _layer;

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object in the specified layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
            {
                Vector3 newPosition = hit.point;

                newPosition.y = hit.point.y + 1f; 

                transform.position = newPosition;
            }
        }
    }
}
