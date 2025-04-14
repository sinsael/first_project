using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI; // 대화 UI
    public TextMeshProUGUI dialogueText; // 대화 텍스트
    private string[] stage1Dialogue;
    private string[] stage2Dialogue; // 스테이지 2 대화
    private string[] stage3Dialogue; // 스테이지 3 대화
    private string[] endDialogue; // 엔딩 대화

    int currentLine = 0; // 현재 라인
    int currentStage = 1; // 현재 스테이지 (1, 2, 3 등)

    [SerializeField] float typingSpeed = 0.05f; // 텍스트 타이핑 속도

    public void Start()
    {
        dialogueUI.SetActive(false); // 시작 시 대화 UI 비활성화
    }


    // 대화 시작 함수 (스테이지 번호에 맞는 대화 시작)
    public void StartDialogue(int stage)
    {
        currentStage = stage;
        currentLine = 0; // 대화 초기화
        string[] dialogueLines = GetcurrentDialogue(); 
        StartStageDialogue(dialogueLines); // 스테이지에 맞는 대화 시작
    }

    // 스테이지에 맞는 대화 시작
    public void StartStageDialogue(string[] dialogueLines)
    {
        dialogueUI.SetActive(true); // 대화 UI 활성화
        Time.timeScale = 0; // 게임 시간 멈추기
        StartCoroutine(TypeSentence(dialogueLines[currentLine])); // 첫 번째 대화 타이핑 시작
    }

    // 텍스트 타이핑 효과
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // 텍스트 초기화
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도 대기
        }
    }

    // 다음 대화로 넘어가기
    public void NextLine()
    {
        currentLine++;
        string[] currentDialogue = GetcurrentDialogue(); // 현재 스테이지에 맞는 대화 배열 선택
        // 다음 대화로 넘어가기
        if (currentLine < currentDialogue.Length)
        {
            StartCoroutine(TypeSentence(currentDialogue[currentLine]));
        }
        else
        {
            EndDialogue(); // 대화 종료
        }
    }

    public string[] GetcurrentDialogue()
    {
        switch (currentStage)
        {
            case 1:
                return stage1Dialogue;
            case 2:
                return stage2Dialogue;
            case 3:
                return stage3Dialogue;
            case 4:
                return endDialogue;
            default:
                return null;
        }
    }

    // 대화 종료
    public void EndDialogue()
    {
        dialogueUI.SetActive(false); // 대화 UI 숨기기
        Time.timeScale = 1; // 게임 시간 정상 흐름
        Debug.Log("대화 끝, 게임 시작");
    }

    // 스킵
    public void SkipDialogue()
    {
        // 현재 스테이지에 맞는 대화 배열 선택
        string[] currentDialogue = GetcurrentDialogue();
        // 대화가 끝나지 않았다면 마지막 대화로 이동
        if (currentLine < currentDialogue.Length - 1)
        {
            currentLine = currentDialogue.Length - 1; // 마지막 대화로 이동
            EndDialogue(); // 대화 종료
        }
    }
}
