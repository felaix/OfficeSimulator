using TMPro;
using UnityEngine;

public class ProjectItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameTitleTMP;
    [SerializeField] private TMP_Text _gameProgressTMP;
    [SerializeField] private TMP_Text _gameDesignTMP;
    [SerializeField] private TMP_Text _gameProgrammingTMP;
    [SerializeField] private TMP_Text _gameBugsTMP;
    [SerializeField] private TMP_Text _gameRevenueTMP;

    public int ID;

    public void SetTitle(string value) => _gameTitleTMP.text = value;
    public void SetProgress(string value) => _gameProgressTMP.text = value;
    public void SetDesign(string value) => _gameDesignTMP.text = value;
    public void SetProgramming(string value) => _gameProgrammingTMP.text = value;
    public void SetBugs(string value) => _gameBugsTMP.text = value;
    public void SetRevenue(string value) => _gameRevenueTMP.text = value;

}
