using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverUITween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private RectTransform rect;
    private Vector3 originalScale;
    private Button btn; 
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float animDuration = .2f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalScale = transform.localScale;
        btn = GetComponent<Button>();

        if (btn != null)
        {
            btn.onClick.AddListener(() => ClickTween());
        }
    }

    private void ClickTween()
    {
        rect.DOScale(originalScale * hoverScale, animDuration).SetEase(Ease.InOutBack);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOScale(originalScale * hoverScale, animDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOScale(originalScale, animDuration).SetEase(Ease.OutBack);
    }
}
