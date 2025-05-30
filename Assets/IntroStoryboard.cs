using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroStoryboardInput : MonoBehaviour
{
    public GameObject[] slides;             // Assign your slides here
    [TextArea(3, 5)]
    private string[] slideTexts = new string[]
    {

    "Ready?",
    // Slide 0: Text only intro
    "Welcome to capitalism. Time to sell your soul for the system.",

    // Slide 1: Hyped player first job
    "YES! I finally got my first job!",

    // Slide 2: 10 years later
    "(20 years later...) Look at their empire. Meanwhile, I'm burnt out. I did everything for them.",

    // Slide 3: Burnout player
    "I'm exhausted. Is this what freedom feels like? I can't even pay my own bills.",

    // Slide 4: Text only outro (if needed)
    "Capitalism wins by breaking you."
    };


    private int currentSlide = 0;

    private PlayerInput playerInput;
    private InputAction nextSlideAction;

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        nextSlideAction = playerInput.actions["NextSlide"];
        nextSlideAction.performed += HandleInput;
    }

    void OnEnable() => nextSlideAction.Enable();
    private void OnDisable()
    {
        nextSlideAction.performed -= HandleInput;
        nextSlideAction.Disable();
    }

    private void HandleInput(InputAction.CallbackContext context)
    {
        if (isTyping)
        {
            CompleteTyping();
        }
        else
        {
            NextSlide();
        }
    }

    void Start()
    {
        foreach (GameObject slide in slides)
            slide.SetActive(false);

        ShowSlide(currentSlide);
    }

    void ShowSlide(int index)
    {
        if (index < 0 || index >= slides.Length) return;

        slides[index].SetActive(true);

        var vp = slides[index].GetComponent<VideoPlayer>();
        if (vp != null)
            vp.Play();

        var textComponent = slides[index].GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null && slideTexts != null && index < slideTexts.Length)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            textComponent.text = "";
            isTyping = true;
            typingCoroutine = StartCoroutine(TypeText(textComponent, slideTexts[index], 1.5f));
        }
    }

    IEnumerator TypeText(TextMeshProUGUI textComponent, string fullText, float duration)
    {
        float t = 0f;
        int length = fullText.Length;

        while (t < duration)
        {
            int charCount = Mathf.Clamp(Mathf.FloorToInt((t / duration) * length), 0, length);
            textComponent.text = fullText.Substring(0, charCount);
            t += Time.deltaTime;
            yield return null;
        }

        textComponent.text = fullText;
        isTyping = false;
        typingCoroutine = null;
    }

    void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            var textComponent = slides[currentSlide].GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null && slideTexts != null && currentSlide < slideTexts.Length)
                textComponent.text = slideTexts[currentSlide];

            isTyping = false;
            typingCoroutine = null;
        }
    }

    void NextSlide()
    {
        if (currentSlide < slides.Length)
            slides[currentSlide].SetActive(false);

        currentSlide++;

        if (currentSlide < slides.Length)
        {
            ShowSlide(currentSlide);
        }
        else
        {
            // Alle Slides durch, Szene laden
            SceneManager.LoadScene("Office");
        }
    }


}