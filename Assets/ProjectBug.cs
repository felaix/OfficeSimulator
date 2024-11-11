using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectBug : MonoBehaviour
{
    public BugType _bugType;
    public string _bugDescription;

    public TMP_Text _descriptionTMP;
    public TMP_Text _timeTMP;
    public Button _fixButton;

    private void Start()
    {
        _descriptionTMP.text = _bugDescription;

    }
}

public enum BugType
{
    critical,
    medium,
    low
}
