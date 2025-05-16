using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMessageBox : MonoBehaviour
{
    public static GameMessageBox Instance { get; private set; }

    [SerializeField] private TMP_Text tmp;
    [SerializeField] private GameObject root;
    [SerializeField] private Image iconIMG;
    [SerializeField] private GameObject errorBG;
    [SerializeField] private Button okButton;

    public Sprite coinSprite;

    private CanvasGroup canvasGroup;
    private UnityAction onAcceptAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        canvasGroup = root.GetComponent<CanvasGroup>();
        root.SetActive(false);

        okButton.onClick.AddListener(OnOkPressed);
    }

    public void ShowErrorNotEnoughMoney()
    {
        Show("Not enough Money", coinSprite, true);
    }

    public void Show(string msg, Sprite icon = null, bool error = true, UnityAction onOk = null)
    {
        tmp.text = msg;

        iconIMG.sprite = icon;
        onAcceptAction = onOk;

        iconIMG.gameObject.SetActive(icon != null);
        errorBG.SetActive(error);

        root.SetActive(true);
        root.transform.localScale = Vector3.one * .8f;
        canvasGroup.alpha = 0f;

        Sequence anim = DOTween.Sequence();
        anim.Append(canvasGroup.DOFade(1f, .2f));
        anim.Join(root.transform.DOScale(1f, .3f).SetEase(Ease.OutBack));
    }

    private void OnOkPressed()
    {
        root.SetActive(false);
        onAcceptAction?.Invoke();
        onAcceptAction = null; // clear
    }
}
