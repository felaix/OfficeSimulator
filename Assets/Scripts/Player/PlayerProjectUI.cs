using TMPro;
using UnityEngine;

public class PlayerProjectUI : MonoBehaviour
{
    public static PlayerProjectUI Instance { get; private set; }

    [Header("Fenster & Container")]
    [SerializeField] private Transform createProjectWindow;
    [SerializeField] private Transform projectContainer;
    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private TMP_InputField titleInput;

    private GameObject _selectedProjectTypeIcon;
    private GameObject _selectedMarketingIcon;
    private GameObject _selectedPolicyIcon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        createProjectWindow.gameObject.SetActive(false);
        projectContainer.gameObject.SetActive(false);
    }

    public void ClearAllSelections()
    {
        if (_selectedProjectTypeIcon != null) _selectedProjectTypeIcon.SetActive(false);
        if (_selectedMarketingIcon != null) _selectedMarketingIcon.SetActive(false);
        if (_selectedPolicyIcon != null) _selectedPolicyIcon.SetActive(false);

        _selectedProjectTypeIcon = null;
        _selectedMarketingIcon = null;
        _selectedPolicyIcon = null;

        descriptionTMP.text = "";
        titleInput.text = ""; // Titelfeld ebenfalls zurücksetzen
    }

    public void UpdateFullDescription(string newDesc)
    {
        descriptionTMP.text = newDesc;
    }

    // Live-Updates: siehe vorherige Version
    public void UpdateProjectTypeDescription(ProjectType type)
    {
        switch (type)
        {
            case ProjectType.TrashGame:
                descriptionTMP.text = "Trash Game: Schnelles Spiel mit fragwürdiger Qualität.";
                break;
            case ProjectType.FakeSocialMedia:
                descriptionTMP.text = "Fake Social Media: Sammle Nutzerdaten unter falscher Flagge.";
                break;
            case ProjectType.FakeOnlineShop:
                descriptionTMP.text = "Fake Online Shop: Täusche Kunden mit nicht existierenden Produkten.";
                break;
            case ProjectType.MailScam:
                descriptionTMP.text = "Mail Scam: Erstelle Phishing-Mails und betrüge ahnungslose Nutzer.";
                break;
            default:
                descriptionTMP.text = "Wähle zuerst einen Projekttyp.";
                break;
        }
    }

    public void UpdateMarketingDescription(MarketingStrategy marketing)
    {
        switch (marketing)
        {
            case MarketingStrategy.BlockchainIntegration:
                descriptionTMP.text = "Blockchain Integration: Täusche Innovation mit leerem Buzzword-Hype.";
                break;
            case MarketingStrategy.NFT:
                descriptionTMP.text = "NFT: Verkaufe nutzlose Pixel als Sammlerstücke.";
                break;
            case MarketingStrategy.Pay2Win:
                descriptionTMP.text = "Pay2Win: Zahle, um im Spiel zu gewinnen.";
                break;
            case MarketingStrategy.DataHarvesting:
                descriptionTMP.text = "Data Harvesting: Verkaufe Nutzerdaten weiter.";
                break;
            case MarketingStrategy.ReferralPyramidSystem:
                descriptionTMP.text = "Referral Pyramid System: Locke Nutzer mit Versprechen von hohen Gewinnen.";
                break;
            case MarketingStrategy.FakeReviews:
                descriptionTMP.text = "Fake Reviews: Bestich Influencer für glattgebügelte Bewertungen.";
                break;
            default:
                descriptionTMP.text = "Wähle zuerst eine Marketing-Strategie.";
                break;
        }
    }

    public void UpdatePolicyDescription(EmployeePolicy policy)
    {
        switch (policy)
        {
            case EmployeePolicy.CheapFreelancer:
                descriptionTMP.text = "Cheap Freelancer: Leih dir Billigarbeitskräfte aus aller Welt.";
                break;
            case EmployeePolicy.UnpaidInterns:
                descriptionTMP.text = "Unpaid Interns: Niemand zahlt mehr – Praktikanten schuften gratis.";
                break;
            case EmployeePolicy.CrunchTime:
                descriptionTMP.text = "Crunch Time: Team muss Tag und Nacht durcharbeiten.";
                break;
            case EmployeePolicy.FakeWellnessProgram:
                descriptionTMP.text = "Fake Wellness Program: Stelle Gym-Sessions auf, aber keiner darf hingehen.";
                break;
            case EmployeePolicy.CultureOfFear:
                descriptionTMP.text = "Culture of Fear: Bedrohungsszenarien, damit niemand kündigt.";
                break;
            case EmployeePolicy.PyramidReferrals:
                descriptionTMP.text = "Pyramid Referrals: Zahl den Leuten, die neue Leute rekrutieren.";
                break;
            default:
                descriptionTMP.text = "Wähle zuerst eine Mitarbeiter-Policy.";
                break;
        }
    }

    public void SelectProjectTypeIcon(GameObject icon)
    {
        if (_selectedProjectTypeIcon != null)
            _selectedProjectTypeIcon.SetActive(false);

        _selectedProjectTypeIcon = icon;
        icon?.SetActive(true);
    }

    public void SelectMarketingIcon(GameObject icon)
    {
        if (_selectedMarketingIcon != null)
            _selectedMarketingIcon.SetActive(false);

        _selectedMarketingIcon = icon;
        icon?.SetActive(true);
    }

    public void SelectPolicyIcon(GameObject icon)
    {
        if (_selectedPolicyIcon != null)
            _selectedPolicyIcon.SetActive(false);

        _selectedPolicyIcon = icon;
        icon?.SetActive(true);
    }

    public void ToggleProjectList(bool isVisible)
    {
        projectContainer.gameObject.SetActive(isVisible);
    }

    public void ToggleCreateWindow(bool isVisible)
    {
        createProjectWindow.gameObject.SetActive(isVisible);
    }

    /// <summary>
    /// Getter, um den aktuell im InputField eingegebenen Titel abzurufen.
    /// </summary>
    public string GetEnteredTitle()
    {
        return titleInput.text.Trim();
    }
}
