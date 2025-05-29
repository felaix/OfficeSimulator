using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectPolicyButton : MonoBehaviour
{
    [Header("Icon, das angezeigt wird, wenn dieser Button aktiv ist")]
    [SerializeField] private GameObject selectIcon;

    [Header("Dieser Button setzt diese Employee-Policy")]
    public EmployeePolicy Policy;

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
        _button.onClick.AddListener(OnClick_SelectPolicy);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick_SelectPolicy);
    }

    private void OnClick_SelectPolicy()
    {
        // 1) Icon dieser Kategorie umschalten
        _ui.SelectPolicyIcon(selectIcon);

        // 2) Strategy-Objekt sicherstellen
        if (_manager.selectedStrategy == null)
            _manager.selectedStrategy = new PlayerProjectStrategy();

        // 3) Employee-Policy setzen
        _manager.selectedStrategy.Policy = Policy;

        // 4) Live-Beschreibung für Policy aktualisieren
        _ui.UpdatePolicyDescription(Policy);
    }
}
