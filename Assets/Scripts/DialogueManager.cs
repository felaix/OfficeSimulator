using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject root;
    [SerializeField] private Button continueBtn;
    [SerializeField] private TMP_Text tmp;
    [SerializeField] private Dialogue[] _dialogue;

    private bool canContinue = false;
    private int index;

    private void Awake()
    {
        Instance = this;

        continueBtn.onClick.AddListener(() => ContinueDialogue(_dialogue));
    }

    private void OnEnable()
    {
        ContinueDialogue(_dialogue);
    }

    public void CreateDialogue(string dialogue)
    {
        root.SetActive(true);
        tmp.text = dialogue;
    }

    public void ContinueDialogue(Dialogue[] dialogues = null)
    {

        if (dialogues == null) { dialogues = _dialogue; }

        if (index >= _dialogue.Length)
        {
            root.SetActive(false); return;
        }
        else
        {
            if (_dialogue[index] == null) return;

            if (_dialogue[index].AutoContinue)
            {
                root.SetActive(true);
                tmp.text = _dialogue[index].text;
                index++;
            }else if (canContinue)
            {
                root.SetActive(true);
                tmp.text = _dialogue[index].text;
                canContinue = false;
                index++;
            }else
            {
                root.SetActive(false);
                canContinue = true;
            }

        }

    }

}

[System.Serializable]
public class Dialogue
{
    public bool AutoContinue = false;
    public string text = "";
}
