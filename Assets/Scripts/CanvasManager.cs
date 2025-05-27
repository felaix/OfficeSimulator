using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(0)]
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField] private GameObject actionBarUIPrefab;
    [SerializeField] private GameObject actionBarButtonPrefab;
    [SerializeField] private GameObject _ux;
    [SerializeField] private List<GameObject> _infoObjects;

    [SerializeField] private TMP_Text playerMoneyTMP;
    [SerializeField] private TMP_Text gameClockTMP;
    [SerializeField] private float animDuration = 2f;

    public GameObject MainCanvas;

    private GameObject currentActionBar;
    private PlayerInputActions input;
    private bool infoIsActive = true;

    public void RemoveActionBar() { if (currentActionBar != null) Destroy(currentActionBar); }

    public void ToggleUX() { _ux.SetActive(!_ux.gameObject.activeSelf); }

    private void OnEnable()
    {
        GameClock.OnNewMonth += UpdateDateTMP;
        input = new PlayerInputActions();

        input.Game.ToggleInfo.performed += ToggleInfo_performed;

        input.Enable();
    }

    private void ToggleInfo_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("hide info");

        if (infoIsActive)
        {
            HideInfo();
            infoIsActive = false;
        }
        else
        {
            ShowInfo();
            infoIsActive = true;
        }
    }

    public void HideInfo()
    {
        foreach (GameObject item in _infoObjects)
        {
            item.SetActive(false);
        }
    }

    public void ShowInfo()
    {
        foreach (GameObject item in _infoObjects)
        {
            item.SetActive(true);
        }
    }

    private void UpdateDateTMP(int year, int month)
    {
        gameClockTMP.SetText(year.ToString() + "-" + month.ToString());
    }

    public void UpdatePlayerStats(Stats stats)
    {

    }

    public void UpdatePlayerMoney(Color highlightColor, int value, int amount)
    {
        Color originalColor = playerMoneyTMP.color;

        playerMoneyTMP.SetText(value.ToString());

        // Immediately set to highlight color and new value
        playerMoneyTMP.color = highlightColor;
        playerMoneyTMP.text = amount.ToString();

        // Create the animation sequence
        Sequence sequence = DOTween.Sequence();

        // 1. First animate the subtraction value (optional scale effect)
        sequence.Append(playerMoneyTMP.transform.DOScale(1.1f, animDuration * 0.3f));
        sequence.Append(playerMoneyTMP.transform.DOScale(1f, animDuration * 0.2f));

        // 2. Then change to the new value and original color
        sequence.AppendCallback(() => {
            playerMoneyTMP.text = value.ToString();
            playerMoneyTMP.color = originalColor;
        });

        // 3. Optional: Add a little bounce effect when showing the new value
        sequence.Append(playerMoneyTMP.transform.DOScale(1.1f, animDuration * 0.15f));
        sequence.Append(playerMoneyTMP.transform.DOScale(1f, animDuration * 0.15f));

    }

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
