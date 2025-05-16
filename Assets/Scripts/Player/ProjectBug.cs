using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectBug : MonoBehaviour
{
    public BugType _bugType;

    public TMP_Text _descriptionTMP;
    public TMP_Text _timeTMP;
    public Button _fixButton;

    public Bug bugItem;

    private void Start()
    {
        _fixButton.onClick.AddListener(() => FixBug());
    }

    private void FixBug()
    {
        bugItem.Project.FixBug(bugItem);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    public void InitializeBugUI(Bug bug)
    {
        bugItem = bug;
        _descriptionTMP.text = _bugType.ToString();

        switch (_bugType)
        {
            case BugType.CRITICAL:
                _timeTMP.text = "8h";
                break;
            case BugType.BALANCING:
                _timeTMP.text = "12h";
                break;
            default:
                break;
        }
    }

}

public enum BugType
{
    CRITICAL,
    BALANCING
}
