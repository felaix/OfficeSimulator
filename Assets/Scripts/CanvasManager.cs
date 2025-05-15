using DG.Tweening;
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
        Vector3 mousePos = Input.mousePosition;

        currentActionBar = Instantiate(actionBarUIPrefab, mousePos, Quaternion.identity, MainCanvas.transform);
        return currentActionBar;
    }

    public Button CreateActionButton(string text)
    {
        Button btn = Instantiate(actionBarButtonPrefab, currentActionBar.transform).GetComponent<Button>();
        TMP_Text tmpText = btn.GetComponentInChildren<TMP_Text>();
        tmpText.SetText(text);

        btn.transform.localScale = Vector3.zero; // Start unsichtbar
        btn.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack); // sanft skalieren

        btn.onClick.AddListener(() => RemoveActionBar());
        return btn;
    }


    private void Awake()
    {
        Instance = this;
    }


}
