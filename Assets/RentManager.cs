using UnityEngine;

public class RentManager : MonoBehaviour
{
    public int[] rentPerMonth = { 0, 100, 200, 300 };
    public Unlock roomUnlocker;

    private int currentRoomLevel = -1;

    private void OnEnable()
    {
        GameClock.OnNewMonth += PayRent;
    }

    private void PayRent(int year, int month)
    {
        if (currentRoomLevel >= 0)
        {
            int rent = rentPerMonth[currentRoomLevel];
            PlayerInventory.Instance.DeductMoney(rent);
        }
    }

    private void OnDisable()
    {
        GameClock.OnNewMonth -= PayRent;
    }

    public void RentRoom(int level)
    {
        if (level < 0 || level >= roomUnlocker.roomsToUnlock.Length) return;

        currentRoomLevel = level;
        roomUnlocker.UnlockRoom(level);
    }
}
