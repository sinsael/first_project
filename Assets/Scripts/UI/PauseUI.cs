using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    CanvasGroup canvasGroup; // CanvasGroup 컴포넌트
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup 컴포넌트 가져오기
        // UI 비활성화
        gameObject.SetActive(false); // 현재 UI 비활성화
        DontDestroyOnLoad(gameObject);
    }
    public void ContinueButton()
    {
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        SetCanvasGroupAlpha(0f); // 일시정지 화면 투명하게 만들기
        StartCoroutine(ContinueGameAfterDelay(3f)); // 3초 대기 후 게임 재개
    }

    // 게임 재시작 메서드
    public void RestartStageButton()
    {
        Debug.Log("게임 재시작");
        StartCoroutine(RestartAfterSceneLoad());
    }

    // 3초 대기후 게임 재개 메서드
    IEnumerator ContinueGameAfterDelay(float delay)
    {
        Debug.Log("게임 재개 대기 중..."); // 디버그 메시지 출력
        yield return new WaitForSecondsRealtime(delay); // 3초 대기
        // 일시정지 화면 투명하게 만들기
        Debug.Log("대기 시간 : " + delay + "초"); // 대기 시간 출력
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        ContinueGame(); // 게임 재개
    }

    // 게임 재개 메서드
    void ContinueGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        GameManager.Instance.currentGameState = GameManager.GameState.Playing; // 게임 상태 변경
        SetCanvasGroupAlpha(1f); // UI 투명도 설정
        /// UI 비활성화
        UIManager.Instance.HideUI(UIManager.UIType.PauseUI);
    }


    IEnumerator RestartAfterSceneLoad()
    {
        GameManager.Instance.currentGameState = GameManager.GameState.Start; // 게임 상태 변경
        Time.timeScale = 0f; // 게임 속도 정지
        Scene current = SceneManager.GetActiveScene();
        UIManager.Instance.HideUI(UIManager.UIType.PauseUI); // 일시정지 UI 비활성화
        SceneManager.LoadScene(current.name);
        yield return null; // 한 프레임 기다리기 (씬 전환 완료까지 대기)
    }

    public void GoToMainMenu()
    {
        // 페이드 아웃 메서드
        UIManager.Instance.FadeOut(1f); // 페이드 아웃 시작
        // DontDestroyOnLoad로 살아있는 오브젝트들을 찾는다
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.name == "DontDestroyOnLoad")
            {
                Destroy(obj);
            }
        }

        // 메인 메뉴 씬으로 이동
        SceneManager.LoadScene("Lobby");
    }

    // 캔버스그룹 투명도 설정
    public void SetCanvasGroupAlpha(float alpha)
    {
        canvasGroup.alpha = alpha; // 알파 값 설정
        // 캔버스 그룹의 상호작용 설정
        canvasGroup.interactable = alpha > 0; // 알파 값이 0보다 클 때 상호작용 가능
        // 캔버스 그룹의 블록 설정
        canvasGroup.blocksRaycasts = alpha > 0; // 알파 값이 0보다 클 때 블록 가능
    }
    
}
