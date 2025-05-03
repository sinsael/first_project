using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : DefualtUI
{
    CanvasGroup canvasGroup; // CanvasGroup 컴포넌트
                             // 싱글톤 
    public static PauseUI Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (UIManager.Instance != null)
        {
            UIManager.Instance.PauseUI = this.gameObject;
            if (UIManager.Instance.uidict != null)
            {
                UIManager.Instance.uidict[UIManager.UIType.PauseUI] = this.gameObject;
            }
        }


    }

    public override void ContinueButton()
    {
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        SetCanvasGroupAlpha(0f); // 일시정지 화면 투명하게 만들기
        StartCoroutine(ContinueGameAfterDelay(3f)); // 3초 대기 후 게임 재개
    }

    // 게임 재시작 메서드
    public override void RestartStageButton()
    {
        Debug.Log("게임 재시작");
        StartCoroutine(RestartAfterSceneLoad());
    }



    // 게임 재개 메서드
    public override void ContinueGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        GameManager.Instance.currentGameState = GameManager.GameState.Playing; // 게임 상태 변경
        SetCanvasGroupAlpha(1f); // UI 투명도 설정
        /// UI 비활성화
        UIManager.Instance.HideUI(UIManager.UIType.PauseUI);
    }


    public override IEnumerator RestartAfterSceneLoad()
    {
        GameManager.Instance.currentGameState = GameManager.GameState.Start; // 게임 상태 변경
        Time.timeScale = 0f; // 게임 속도 정지
        Scene current = SceneManager.GetActiveScene();
        ScoreManager.Instance.ResetScore();
        ScoreManager.Instance.ResetMiss();
        UIManager.Instance.HideUI(UIManager.UIType.PauseUI); // 일시정지 UI 비활성화
        SceneManager.LoadScene(current.name);
        yield return null; // 한 프레임 기다리기 (씬 전환 완료까지 대기)
    }

    // 캔버스그룹 투명도 설정
    void SetCanvasGroupAlpha(float alpha)
    {
        canvasGroup.alpha = alpha; // 알파 값 설정
        // 캔버스 그룹의 상호작용 설정
        canvasGroup.interactable = alpha > 0; // 알파 값이 0보다 클 때 상호작용 가능
        // 캔버스 그룹의 블록 설정
        canvasGroup.blocksRaycasts = alpha > 0; // 알파 값이 0보다 클 때 블록 가능
    }

}