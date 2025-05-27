using UnityEngine;
using UnityEngine.UI;

public class CreateProject : MonoBehaviour
{
    public ProjectType type;
    public PCData pcData;
    public CreateProjectButton createProjectButtons;

}

public enum MarketingStrategy
{
    BlockchainIntegration,
    NFT,
    Pay2Win,
    DataHarvesting,
    ReferralPyramidSystem,
    FakeReviews,
    None
}

public enum EmployeePolicy
{
    CheapFreelancer,
    UnpaidInterns,
    CrunchTime,
    FakeWellnessProgram,
    CultureOfFear,
    PyramidReferrals,
    None
}

public class SelectProjectTypeButton
{
    public bool isSelected = false;
    public Button btn;
    public Sprite icon;
    public MarketingStrategy strategy;
    public EmployeePolicy policy;
}