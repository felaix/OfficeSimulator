using UnityEngine;

public class Unlock : MonoBehaviour
{
    public GameObject[] roomsToUnlock;

    public void UnlockRoom(int level)
    {
        switch (level)
        {
            case 0:
                roomsToUnlock[0].SetActive(false); break;
            // - $100 / mo
            case 1:
                roomsToUnlock[1].SetActive(false); break;
            // - $200 / mo
            case 2:
                roomsToUnlock[2].SetActive(false); break;
            // - $300 / mo
            case 3:
                roomsToUnlock[3].SetActive(false); break;
        }
    }
}
