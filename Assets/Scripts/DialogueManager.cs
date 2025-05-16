using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private Button continueBtn;
    [SerializeField] private TMP_Text tmp;
    [SerializeField] private string[] dialogue;

    private int index;

    private void Awake()
    {
        continueBtn.onClick.AddListener(() => ContinueDialogue());
    }

    private void OnEnable()
    {
        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (index >= dialogue.Length)
        {
            root.SetActive(false);
        }else
        {
            root.SetActive(true);
            tmp.text = dialogue[index];
            index++;
        }

    }

}
