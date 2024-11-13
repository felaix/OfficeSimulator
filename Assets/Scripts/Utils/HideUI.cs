using UnityEngine;
using UnityEngine.EventSystems;

public class HideUI : MonoBehaviour
{
    public Canvas canvas;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("World Click");

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider == null || hit.collider != gameObject)
                    {
                        HideAllWindows();
                    }else
                    {
                        Debug.Log(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    private void HideAllWindows()
    {
        bool isFirst = true; // Flag to track the first child

        foreach (Transform child in canvas.transform)
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

}
