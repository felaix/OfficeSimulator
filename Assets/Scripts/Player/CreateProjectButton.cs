using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CreateProjectButton : MonoBehaviour
{
    [SerializeField] private GameObject selectIcon;

    public PlayerProjectStrategy strategies;

    private Button btn;

    private bool isProjectType;
    private bool isMarketingType;
    private bool isEmployeeType;

    private void OnEnable()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => SelectItem());
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        selectIcon.SetActive(false); // Toggle Selection at the beginning

        if (strategies.Type == ProjectType.None) { isProjectType = false; } else isProjectType = true;
        if (strategies.Marketing == MarketingStrategy.None) { isMarketingType = false; } else { isMarketingType = true; }
        if (strategies.Policy == EmployeePolicy.None) { isEmployeeType = false; } else { isEmployeeType = true; }
    }

    public void SetType()
    {

        // Select Project Type
        if (isProjectType)
        {
            PlayerProjectManager.Instance.UpdatePlayerProjectType(strategies.Type);
            PlayerProjectUI.Instance.SelectProjectType(selectIcon);
            return;
        }

        // Select Marketing Type
        if (isMarketingType)
        {
            PlayerProjectManager.Instance.UpdatePlayerProjectMarketingStrategy(strategies.Marketing);
            PlayerProjectUI.Instance.SelectMarketingType(selectIcon);
            return;
        }


        // Select Employee Policy
        if (isEmployeeType)
        {
            PlayerProjectManager.Instance.UpdatePlayerProjectEmployeePolicy(strategies.Policy);
            PlayerProjectUI.Instance.SelectPolicyType(selectIcon);
            return;
        }

        // Set Description
    }

    private void SelectItem() 
    {
        selectIcon.SetActive(true);
        SetType();
    }

}
