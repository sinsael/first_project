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
    public GameObject NoneUI; // 기본 UI
    public Dictionary<UIType, GameObject> uidict;
    public enum UIType
    {
        ClearUI,
        GameOverUI,
        PauseUI,
        NoneUI // 기본 UI
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

        uidict = new Dictionary<UIType, GameObject>() // UI 딕셔너리 초기화
        {
            { UIType.ClearUI, ClearUI },
            { UIType.GameOverUI, GameOverUI },
            { UIType.PauseUI, PauseUI },
            { UIType.NoneUI, NoneUI }
        };

    }

    void Start()
    {
        // 모든 UI 비활성화
        uidict[UIType.ClearUI].SetActive(false); // 클리어 UI 비활성화
        uidict[UIType.GameOverUI].SetActive(false); // 게임 오버 UI 비활성화
        uidict[UIType.PauseUI].SetActive(false); // 일시 정지 UI 비활성화
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
}
