using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueData dialogueData;
    public DialogueManager dialogueManager;
    //싱글톤
    public static DialogueStarter instance;
    void Start()
    {
        dialogueManager.StartDialogue(dialogueData);
    }
}
