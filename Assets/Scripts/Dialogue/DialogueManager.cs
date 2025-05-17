using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;

    public float typingSpeed = 0.05f; // 타이핑 속도
    public string nextSceneName; // 다음 씬 이름

    private Queue<DialogueLine> dialogueQueue;
    private Coroutine typingCoroutine;

    // 싱글톤
    public static DialogueManager instance;


    void Awake()
    {
        if (dialogueQueue == null)
        {
            dialogueQueue = new Queue<DialogueLine>();
        }
    }

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("dialogue가 null임");
            return;
        }

        if (dialogue.lines == null)
        {
            Debug.LogError("대화 줄이 null임");
            return;
        }

        dialogueQueue = new Queue<DialogueLine>(dialogue.lines);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (nameText == null || dialogueText == null || portraitImage == null)
        {
            Debug.LogError("UI 요소가 null임");
            return;
        }
        if (dialogueQueue == null || dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueQueue.Dequeue();
        if (line == null)
        {
            Debug.LogError("대화 줄이 null임");
            ShowNextLine(); // 건너뛰기
            return;
        }
        nameText.text = line.speakerName ?? "???";

        if (line.portrait != null)
        {
            portraitImage.sprite = line.portrait;
            portraitImage.enabled = true;
        }
        else
        {
            portraitImage.sprite = null;
            portraitImage.enabled = false;
        }

        if (string.IsNullOrEmpty(line.dialogueText))
        {
            Debug.LogWarning("dialogueText가 비었슴.");
            line.dialogueText = " ";
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(line.dialogueText));
    }


    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void SkipDialogue()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void EndDialogue()
    {
        if (dialogueText != null) dialogueText.text = "";
        if (nameText != null) nameText.text = "";
        if (portraitImage != null) portraitImage.sprite = null;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
