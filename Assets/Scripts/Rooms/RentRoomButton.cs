using UnityEngine;
using UnityEngine.UI;

public class RentRoomButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button btn;
    public int level;
    [SerializeField] private RentManager rentManager;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => RentRoom());
    }

    private void RentRoom()
    {
        if (rentManager.RentRoom(level))
        {
            Destroy(gameObject);
        }else // not enough money
        {
        }
    }
}
