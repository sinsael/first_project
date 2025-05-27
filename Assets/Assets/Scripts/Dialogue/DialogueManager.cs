using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public string[] lines;

    int index;

    public float typingSpeed = 0.05f; // 타이핑 속도
    public string nextSceneName; // 다음 씬 이름

    
    // Addressable Asset System
    public string dialogueDataAddress; // Addressable Asset의 주소

    public void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }



    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    public void ShowNextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextLine()
    {
       if(dialogueText.text == lines[index])
        {
            ShowNextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = lines[index];
        }
    }


    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
