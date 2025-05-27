using UnityEngine;

public class RentManager : MonoBehaviour
{
    public Sprite coinSprite;
    public int[] rentPerMonth = { 50, 100, 200, 300 };
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

    public bool RentRoom(int level)
    {
        if (level < 0 || level >= roomUnlocker.roomsToUnlock.Length) return false;

        int rent = rentPerMonth[level];

        if (PlayerInventory.Instance.PlayerStats.Money < rent)
        {
            GameMessageBox.Instance.Show("Not enough Money to rent office room " + level, coinSprite);
            return false;
        }
        else
        {
            PlayerInventory.Instance.DeductMoney(rent);
            GameMessageBox.Instance.Show("Sucess! You now rent room " + level, coinSprite, false);
            DialogueManager.Instance.ContinueDialogue();

            currentRoomLevel = level;
            roomUnlocker.UnlockRoom(level);
            return true;
        }

    }
}
