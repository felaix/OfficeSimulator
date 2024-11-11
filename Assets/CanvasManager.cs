using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField] private GameObject actionBarUIPrefab;
    [SerializeField] private GameObject actionBarButtonPrefab;

    public GameObject MainCanvas;

    private GameObject currentActionBar;

    public void RemoveActionBar(){ if (currentActionBar != null) Destroy(currentActionBar); }

    public GameObject CreateActionBar()
    {
        if (currentActionBar != null) Destroy(currentActionBar);

        currentActionBar = Instantiate(actionBarUIPrefab, MainCanvas.transform);
        return currentActionBar;
    }

    public Button CreateActionButton(string text)
    {
        Button btn = Instantiate(actionBarButtonPrefab, currentActionBar.transform).GetComponent<Button>();
        btn.GetComponentInChildren<TMP_Text>().SetText(text);
        btn.onClick.AddListener(() => RemoveActionBar());
        return btn;
    }

    private void Awake()
    {
        Instance = this;
    }


}
