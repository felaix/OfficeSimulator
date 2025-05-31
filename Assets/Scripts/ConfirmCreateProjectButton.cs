using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ConfirmCreateProjectButton : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] private Transform projectContainerTransform; // Wo das Projekt-UI hingehört

    [Header("Animationseinstellungen")]
    [SerializeField] private float clickScaleAmount = 0.95f;
    [SerializeField] private float clickAnimationDuration = 0.1f;

    private Button _button;
    private PlayerProjectUI _ui;
    private PlayerProjectManager _manager;
    private Vector3 _originalScale;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        // Singletons hier holen, falls sie noch nicht gesetzt sind
        _ui = PlayerProjectUI.Instance;
        _manager = PlayerProjectManager.Instance;

        _button.onClick.AddListener(OnClick_CreateProject);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick_CreateProject);
    }

    private void OnClick_CreateProject()
    {
        // 1) Prüfen, ob UI-Instanz existiert
        if (_ui == null)
        {
            Debug.LogError($"[{nameof(ConfirmCreateProjectButton)}] PlayerProjectUI.Instance ist null! Stelle sicher, dass das UI-Objekt in der Szene existiert und aktiv ist.");
            return;
        }

        // 2) Prüfen, ob Manager-Instanz existiert
        if (_manager == null)
        {
            Debug.LogError($"[{nameof(ConfirmCreateProjectButton)}] PlayerProjectManager.Instance ist null! Stelle sicher, dass der Manager in der Szene aktiv ist.");
            return;
        }

        // 3) Prüfen, ob projectContainerTransform zugewiesen ist
        if (projectContainerTransform == null)
        {
            Debug.LogError($"[{nameof(ConfirmCreateProjectButton)}] projectContainerTransform ist nicht gesetzt! Ziehe im Inspector das Container-Transform für neue Projekte hinein.");
            return;
        }

        var strat = _manager.selectedStrategy;
        if (strat == null)
        {
            Debug.LogError("Keine Strategy initialisiert!");
            return;
        }

        // 4) Validierung: Projekttyp
        if (strat.Type == ProjectType.None)
        {
            Debug.LogError("Kein Projekttyp gewählt!");
            _ui.UpdateProjectTypeDescription(ProjectType.None);
        }

        // 5) Validierung: Marketing
        if (strat.Marketing == MarketingStrategy.None)
        {
            Debug.LogError("Keine Marketing-Strategie gewählt!");
            _ui.UpdateMarketingDescription(MarketingStrategy.None);
        }

        // 6) Validierung: Policy
        if (strat.Policy == EmployeePolicy.None)
        {
            Debug.LogError("Keine Mitarbeiter-Policy gewählt!");
            _ui.UpdatePolicyDescription(EmployeePolicy.None);
        }

        // 7) Validierung: Titel darf nicht leer sein
        string enteredTitle = _ui.GetEnteredTitle();
        if (string.IsNullOrEmpty(enteredTitle))
        {
            Debug.LogError("Bitte gib einen Titel für das Projekt ein!");
        }

        // 8) Animation starten (nur, wenn dieses GameObject noch aktiv ist)
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateButtonClick());
        }

        // 9) Strategy mit endgültigem Titel aktualisieren
        strat.ProjectTitle = enteredTitle;
        strat.GenerateDescription();

        // 10) Projekt tatsächlich erstellen
        _manager.CreateNewPlayerProject();

        // 11) Create-Fenster schließen und Strategy/Texte zurücksetzen
        _ui.ToggleCreateWindow(false);
        _manager.selectedStrategy = null;
        _ui.ClearAllSelections();
    }

    private IEnumerator AnimateButtonClick()
    {
        // Hochskalieren auf (original * clickScaleAmount)
        float elapsed = 0f;
        Vector3 targetScale = _originalScale * clickScaleAmount;
        while (elapsed < clickAnimationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / clickAnimationDuration);
            transform.localScale = Vector3.Lerp(_originalScale, targetScale, t);
            yield return null;
        }

        // Zurück auf Original
        elapsed = 0f;
        while (elapsed < clickAnimationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / clickAnimationDuration);
            transform.localScale = Vector3.Lerp(targetScale, _originalScale, t);
            yield return null;
        }

        transform.localScale = _originalScale;
    }
}
