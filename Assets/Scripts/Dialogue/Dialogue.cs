using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string dialogueText;
    public Sprite portrait;
}

[CreateAssetMenu(menuName = "Dialogue/Dialogue")]
public class DialogueData : ScriptableObject
{
    public List<DialogueLine> lines;
}