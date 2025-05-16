using UnityEngine;
using System;

public class GameClock : MonoBehaviour
{
    public static GameClock Instance { get; private set; }

    public static event Action<int, int> OnNewMonth; // year, month
    private float timePassed = 0f;

    public float monthDuration = 60f;

    private int currentYear = 0;
    private int currentMonth = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void AdvanceMonth()
    {
        currentMonth++;
        if (currentMonth > 12)
        {
            currentMonth = 1;
            currentYear++;
        }

        OnNewMonth?.Invoke(currentYear, currentMonth);
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= monthDuration)
        {
            timePassed = 0f;
            AdvanceMonth();
        }
    }
}
