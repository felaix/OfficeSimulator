using System.Collections;
using UnityEngine;

public class PlayerProject : MonoBehaviour
{
    public string GameName;
    public int GameID;
    public Genre GameGenre;

    [Header("Points")]
    public float GameDesignPoints;
    public float GameDevPoints;
    public float GameFamePoints;

    public int GameBugs;

    [Header("Time")]
    public float _timer = 100f;

    private Stats playerStats;

    private void Start()
    {
        playerStats = PlayerProjectManager.Instance.GetPlayerStats();
        StartCoroutine(CreateGameCoroutine());
    }

    private IEnumerator CreateGameCoroutine()
    {
        PlayerProjectUI.Instance.CreatePlayerProjectUI();

        Debug.Log("Creating game ...");

        float time = 0f;
        int maxBugs = 5;

        while (time < _timer)
        {
            float speed = playerStats.Speed;

            if (GameBugs < maxBugs && Random.Range(0f, 1f) < 0.1f) // 10% chance per iteration
            {
                GameBugs++;
                Debug.Log("A bug appeared! Total bugs: " + GameBugs);
            }

            GameDesignPoints += playerStats.Design;
            time++;

            PlayerProjectUI.Instance.SetPlayerProjectUIProgress(this, (int)time);
            Debug.Log("Fortschritt: " + time + "%. Punkte: " + GameDesignPoints);
            yield return new WaitForSeconds(100f / speed);
        }

        
        Debug.Log(GameName + "completed. Bugs: " + GameBugs + ", Design Points: " +  GameDesignPoints);


    }

    private IEnumerator PublishGameCoroutine()
    {
        Debug.Log("Publishing game ...");


        yield return new WaitForSeconds(1f);
    }
}

public enum Genre
{
    Horror,
    Action,
    Sports
}
