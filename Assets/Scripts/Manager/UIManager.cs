using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   // 싱글톤 인스턴스
    public static UIManager Instance { get; private set; } // 싱글톤 인스턴스
    // UI 오브젝트들
    public GameObject ClearUI; // 클리어 UI
    public GameObject GameOverUI; // 게임 오버 UI
    public GameObject PauseUI; // 일시 정지 UI
    public GameObject FadeUI; // 페이드 UI
    private Dictionary<UIType, GameObject> uidict;
    private CanvasGroup fadeCanvasGroup; // 페이드 UI의 CanvasGroup 컴포넌트
    public enum UIType
    {
        ClearUI,
        GameOverUI,
        PauseUI,
        FadeUI
    }

    

    void Awake()
    {
         if (Instance == null)
        {
            Instance = this; // 인스턴스 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복된 객체 제거
            return;
        }

        uidict =  new Dictionary<UIType, GameObject>() // UI 딕셔너리 초기화
        {
            { UIType.ClearUI, ClearUI },
            { UIType.GameOverUI, GameOverUI },
            { UIType.PauseUI, PauseUI },
            { UIType.FadeUI, FadeUI }
        };
    }

    void Start()
    {
        // 모든 UI 비활성화
        uidict[UIType.ClearUI].SetActive(false); // 클리어 UI 비활성화
        uidict[UIType.GameOverUI].SetActive(false); // 게임 오버 UI 비활성화
        uidict[UIType.PauseUI].SetActive(false); // 일시 정지 UI 비활성화
        fadeCanvasGroup = FadeUI.GetComponent<CanvasGroup>(); // 페이드 UI의 CanvasGroup 컴포넌트 가져오기
        uidict[UIType.FadeUI].SetActive(false); // 페이드 UI 활성화
    }

    // UI 활성화 메서드
    public void ShowUI(UIType uiType)
    {
        if (uidict.ContainsKey(uiType) && !uidict[uiType].activeSelf) // 해당 UI가 딕셔너리에 존재하는지 확인
        {
            uidict[uiType].SetActive(true); // UI 활성화
        }
    }

    // UI 비활성화 메서드
    public void HideUI(UIType uiType)
    {
       if (uidict.ContainsKey(uiType)) // 해당 UI가 딕셔너리에 존재하는지 확인
        {
            uidict[uiType].SetActive(false); // UI 비활성화
        }
    }

    // 페이드 인 메서드
    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration)); // 페이드 아웃 코루틴 시작
    }

    IEnumerator FadeOutCoroutine(float duration)
    {
        ShowUI(UIType.FadeUI); // 페이드 UI 활성화
        fadeCanvasGroup.alpha = 0f; // 초기 알파 값 설정

        float t = 0f; // 시간 초기화
        while (t < duration) // 지정된 시간 동안 반복
        {
            t += Time.unscaledDeltaTime; // 시간 증가
            fadeCanvasGroup.alpha = Mathf.Clamp01(t / duration); // 알파 값 설정
            yield return null; // 다음 프레임까지 대기
        }
        fadeCanvasGroup.alpha = 1f; // 최종 알파 값 설정
    }

    public void FadeIN(float duration)
    {
        StartCoroutine(FadeInCoroutine(duration)); // 페이드 인 코루틴 시작
    }

    IEnumerator FadeInCoroutine(float duration)
    {
        ShowUI(UIType.FadeUI); // 페이드 UI 활성화
        fadeCanvasGroup.alpha = 1f; // 초기 알파 값 설정

        float t = 0f; // 시간 초기화
        while (t < duration) // 지정된 시간 동안 반복
        {
            t += Time.unscaledDeltaTime; // 시간 증가
            fadeCanvasGroup.alpha = Mathf.Clamp01(1f - t / duration); // 알파 값 설정
            yield return null; // 다음 프레임까지 대기
        }
        fadeCanvasGroup.alpha = 0f; // 최종 알파 값 설정
    }
}
