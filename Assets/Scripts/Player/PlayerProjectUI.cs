using TMPro;
using UnityEngine;

public class PlayerProjectUI : MonoBehaviour
{
    public static PlayerProjectUI Instance { get; private set; }

    [Header("Windows & Containers")]
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

        descriptionTMP.text = "Select your tools of exploitation";
        titleInput.text = "";
    }

    public void UpdateFullDescription(string newDesc)
    {
        descriptionTMP.text = newDesc;
    }

    public void UpdateProjectTypeDescription(ProjectType type)
    {
        switch (type)
        {
            case ProjectType.TrashGame:
                descriptionTMP.text = "Trash Game: Pump out low-effort garbage. Monetize disappointment.";
                break;
            case ProjectType.FakeSocialMedia:
                descriptionTMP.text = "Fake Social Media: Farm user data while pretending to connect people.";
                break;
            case ProjectType.FakeOnlineShop:
                descriptionTMP.text = "Fake Online Shop: Take payments for products that will never ship.";
                break;
            case ProjectType.MailScam:
                descriptionTMP.text = "Mail Scam: Target the vulnerable with too-good-to-be-true offers.";
                break;
            default:
                descriptionTMP.text = "Choose your flavor of exploitation";
                break;
        }
    }

    public void UpdateMarketingDescription(MarketingStrategy marketing)
    {
        switch (marketing)
        {
            case MarketingStrategy.BlockchainIntegration:
                descriptionTMP.text = "Blockchain Hype: Add meaningless tech jargon to inflate perceived value.";
                break;
            case MarketingStrategy.NFT:
                descriptionTMP.text = "NFT Scheme: Sell artificial scarcity for digital trinkets.";
                break;
            case MarketingStrategy.Pay2Win:
                descriptionTMP.text = "Pay2Win: Lock basic functionality behind endless payments.";
                break;
            case MarketingStrategy.DataHarvesting:
                descriptionTMP.text = "Data Harvesting: Package and sell user privacy to the highest bidder.";
                break;
            case MarketingStrategy.ReferralPyramidSystem:
                descriptionTMP.text = "Pyramid Scheme: Profit from recruiting new victims into the system.";
                break;
            case MarketingStrategy.FakeReviews:
                descriptionTMP.text = "Fake Reviews: Manufacture false praise to hide poor quality.";
                break;
            default:
                descriptionTMP.text = "Choose how you'll manipulate the masses";
                break;
        }
    }

    public void UpdatePolicyDescription(EmployeePolicy policy)
    {
        switch (policy)
        {
            case EmployeePolicy.CheapFreelancer:
                descriptionTMP.text = "Cheap Freelancers: Exploit global wage disparities for maximum profit.";
                break;
            case EmployeePolicy.UnpaidInterns:
                descriptionTMP.text = "Unpaid Interns: Extract labor while calling it 'opportunity'.";
                break;
            case EmployeePolicy.CrunchTime:
                descriptionTMP.text = "Crunch Culture: Burn out workers to meet artificial deadlines.";
                break;
            case EmployeePolicy.FakeWellnessProgram:
                descriptionTMP.text = "Fake Wellness: Offer token benefits nobody has time to use.";
                break;
            case EmployeePolicy.CultureOfFear:
                descriptionTMP.text = "Fear Culture: Keep workers compliant through constant insecurity.";
                break;
            case EmployeePolicy.PyramidReferrals:
                descriptionTMP.text = "Recruitment Pyramid: Pay employees to find their own replacements.";
                break;
            default:
                descriptionTMP.text = "Choose how you'll exploit your workforce";
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
        if (isVisible) ClearAllSelections();
    }

    public string GetEnteredTitle()
    {
        return titleInput.text.Trim();
    }
}