using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CreateProjectButton : MonoBehaviour
{
    public enum ButtonCategory { ProjectType, Marketing, Policy }

    [Header("Einstellungen")]
    [SerializeField] private ButtonCategory category;

    [Tooltip("Icon, das beim Auswählen dieses Buttons aufleuchtet")]
    [SerializeField] private GameObject selectIcon;

    [Tooltip("Wert, der gesetzt wird, je nachdem welche Kategorie gewählt ist")]
    [SerializeField] private ProjectType projectType = ProjectType.None;
    [SerializeField] private MarketingStrategy marketingStrategy = MarketingStrategy.None;
    [SerializeField] private EmployeePolicy employeePolicy = EmployeePolicy.None;

    [Header("Animationseinstellungen")]
    [SerializeField] private float iconScaleOnSelect = 1.2f;
    [SerializeField] private float animationDuration = 0.15f;

    private Button _button;
    private PlayerProjectUI _ui;
    private PlayerProjectManager _manager;
    private Vector3 _originalIconScale;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (selectIcon != null)
        {
            selectIcon.SetActive(false);
            _originalIconScale = selectIcon.transform.localScale;
        }
    }

    private void OnEnable()
    {
        // Instanzen hier holen, falls sie beim Awake noch nicht bereit waren
        _ui = PlayerProjectUI.Instance;
        _manager = PlayerProjectManager.Instance;

        _button.onClick.AddListener(OnClick_Select);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick_Select);
    }

    private void OnClick_Select()
    {
        // 1) UI-Instanz prüfen
        if (_ui == null)
        {
            Debug.LogError($"[{nameof(CreateProjectButton)}] PlayerProjectUI.Instance ist null! Stelle sicher, dass das UI-Objekt in der Szene existiert und dieses Script erst danach aktiviert wird.");
            return;
        }

        // 2) Manager-Instanz prüfen
        if (_manager == null)
        {
            Debug.LogError($"[{nameof(CreateProjectButton)}] PlayerProjectManager.Instance ist null! Stelle sicher, dass der Manager in der Szene aktiv ist.");
            return;
        }

        // 3) Icon-Effekt
        if (selectIcon != null)
        {
            selectIcon.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(AnimateIconPulse());
        }

        // 4) Strategy anlegen, falls noch nicht vorhanden
        if (_manager.selectedStrategy == null)
            _manager.selectedStrategy = new PlayerProjectStrategy();

        // 5) Kategorie setzen
        switch (category)
        {
            case ButtonCategory.ProjectType:
                if (projectType == ProjectType.None) return;
                _manager.selectedStrategy.Type = projectType;
                _ui.SelectProjectTypeIcon(selectIcon);
                _ui.UpdateProjectTypeDescription(projectType);
                break;

            case ButtonCategory.Marketing:
                if (marketingStrategy == MarketingStrategy.None) return;
                _manager.selectedStrategy.Marketing = marketingStrategy;
                _ui.SelectMarketingIcon(selectIcon);
                _ui.UpdateMarketingDescription(marketingStrategy);
                break;

            case ButtonCategory.Policy:
                if (employeePolicy == EmployeePolicy.None) return;
                _manager.selectedStrategy.Policy = employeePolicy;
                _ui.SelectPolicyIcon(selectIcon);
                _ui.UpdatePolicyDescription(employeePolicy);
                break;
        }
    }

    private IEnumerator AnimateIconPulse()
    {
        if (selectIcon == null) yield break;

        var iconTransform = selectIcon.transform;
        Vector3 targetScale = _originalIconScale * iconScaleOnSelect;
        float elapsed = 0f;

        // Hochskalieren
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            iconTransform.localScale = Vector3.Lerp(_originalIconScale, targetScale, t);
            yield return null;
        }

        // Zurück skalieren
        elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            iconTransform.localScale = Vector3.Lerp(targetScale, _originalIconScale, t);
            yield return null;
        }

        iconTransform.localScale = _originalIconScale;
    }
}
