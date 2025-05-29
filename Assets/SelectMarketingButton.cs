using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectMarketingButton : MonoBehaviour
{
    [Header("Icon, das angezeigt wird, wenn dieser Button aktiv ist")]
    [SerializeField] private GameObject selectIcon;

    [Header("Dieser Button setzt diese Marketing-Strategie")]
    public MarketingStrategy Marketing;

    private Button _button;
    private PlayerProjectUI _ui;
    private PlayerProjectManager _manager;

    private void Awake()
    {
        _button = GetComponent<Button>();
        selectIcon.SetActive(false);
        _ui = PlayerProjectUI.Instance;
        _manager = PlayerProjectManager.Instance;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick_SelectMarketing);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick_SelectMarketing);
    }

    private void OnClick_SelectMarketing()
    {
        // 1) Icon dieser Kategorie umschalten
        _ui.SelectMarketingIcon(selectIcon);

        // 2) Strategy-Objekt sicherstellen
        if (_manager.selectedStrategy == null)
            _manager.selectedStrategy = new PlayerProjectStrategy();

        // 3) Marketing-Strategie setzen
        _manager.selectedStrategy.Marketing = Marketing;

        // 4) Live-Beschreibung für Marketing aktualisieren
        _ui.UpdateMarketingDescription(Marketing);
    }
}
