using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Instance = this;

            // PauseUI가 null이면 현재 존재하는 싱글톤을 참조
            if (PauseUI == null && global::PauseUI.Instance != null)
            {
                PauseUI = global::PauseUI.Instance.gameObject;
            }

            uidict = new Dictionary<UIType, GameObject>()
        {
            { UIType.ClearUI, ClearUI },
            { UIType.GameOverUI, GameOverUI },
            { UIType.PauseUI, PauseUI },
            { UIType.NoneUI, NoneUI }
        };
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }

    void Start()
    {
        HideAllUI();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideAllUI(); // 씬 로드될 때 UI 전부 끄기
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

    public void HideAllUI()
    {
        foreach (var ui in uidict.Values)
        {
            if (ui != null)
                ui.SetActive(false);
        }

    }
}