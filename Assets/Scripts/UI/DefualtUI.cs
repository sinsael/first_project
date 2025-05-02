using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefualtUI : MonoBehaviour
{
    
   
    public virtual void ContinueButton()
    {
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        StartCoroutine(ContinueGameAfterDelay(3f)); // 3초 대기 후 게임 재개
    }

    // 게임 재시작 메서드
    public virtual void RestartStageButton()
    {
        Debug.Log("게임 재시작");
        StartCoroutine(RestartAfterSceneLoad());
    }

    // 3초 대기후 게임 재개 메서드
    public virtual IEnumerator ContinueGameAfterDelay(float delay)
    {
        Debug.Log("게임 재개 대기 중..."); // 디버그 메시지 출력
        yield return new WaitForSecondsRealtime(delay); // 3초 대기
        // 일시정지 화면 투명하게 만들기
        Debug.Log("대기 시간 : " + delay + "초"); // 대기 시간 출력
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        ContinueGame(); // 게임 재개
    }

    // 게임 재개 메서드
    public virtual void ContinueGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        GameManager.Instance.currentGameState = GameManager.GameState.Playing; // 게임 상태 변경
        /// UI 비활성화
        UIManager.Instance.HideUI(UIManager.UIType.NoneUI); // UI 비활성화
    }


    public virtual IEnumerator RestartAfterSceneLoad()
    {
        GameManager.Instance.currentGameState = GameManager.GameState.Start; // 게임 상태 변경
        Time.timeScale = 0f; // 게임 속도 정지
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        yield return null; // 한 프레임 기다리기 (씬 전환 완료까지 대기)
    }

    public virtual void GoToMainMenu()
    {

        // 현재 활성/비활성 포함 모든 GameObject 찾기 (Resources.FindObjectsOfTypeAll)
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // DontDestroyOnLoad에 있는 오브젝트만
            if (obj.scene.name == "DontDestroyOnLoad" && !obj.hideFlags.HasFlag(HideFlags.NotEditable))
            {
                Destroy(obj);
            }
        }

        // 메인 메뉴 씬으로 이동
        SceneManager.LoadScene("Lobby");
    }
}
