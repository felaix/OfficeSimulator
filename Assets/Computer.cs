using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    private float maxValue = 100;
    private bool isActive = false;
    private Image sliderIMG;
    

    private void Start()
    {
        slider.value = 0;
        slider.maxValue = maxValue;
        slider.gameObject.SetActive(false);
        sliderIMG = slider.fillRect.GetComponent<Image>();
        sliderIMG.color = _color1;
    }

    private void OnEnable()
    {
        GameClock.OnNewMonth += OnNewMonth;
    }

    private void OnDisable()
    {
        GameClock.OnNewMonth -= OnNewMonth;
    }

    public void ToggleProgressUI()
    {
        isActive = !isActive;
        slider.gameObject.SetActive(isActive);
    }

    private void OnNewMonth(int year, int month)
    {
        slider.value += PlayerInventory.Instance.PlayerStats.Speed + 15f;

        if (slider.value >= maxValue)
        {
            slider.value = maxValue;
            sliderIMG.color = _color2;
            ToggleProgressUI();
        }

        if (!isActive && slider.value >= maxValue)
        {
            slider.value = 0;
        }
    }
}
