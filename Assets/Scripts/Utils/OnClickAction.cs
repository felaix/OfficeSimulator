using UnityEngine;
using UnityEngine.Events;

public class OnClickAction : MonoBehaviour
{

    public UnityEvent _action;

    private void OnMouseDown()
    {
        _action.Invoke();
    }
}
