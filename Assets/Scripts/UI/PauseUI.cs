using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void ResumeButton()
    {
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        StartCoroutine(ResumeGameAfterDelay(3f)); // 3초 대기 후 게임 재개
    }

    // 게임 재시작 메서드
    public void RestartStage()
    {
        Debug.Log("게임 재시작");
       StartCoroutine(RestartAfterSceneLoad());
    }

    // 3초 대기후 게임 재개 메서드
    IEnumerator ResumeGameAfterDelay(float delay)
    {
        Debug.Log("게임 재개 대기 중..."); // 디버그 메시지 출력
        yield return new WaitForSecondsRealtime(delay); // 3초 대기
        Debug.Log("대기 시간 : " + delay + "초"); // 대기 시간 출력력
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        ResumeGame(); // 게임 재개
    }

    // 게임 재개 메서드
    void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        GameManager.Instance.currentGameState = GameManager.GameState.Playing; // 게임 상태 변경경
    }


IEnumerator RestartAfterSceneLoad()
{
    Scene current = SceneManager.GetActiveScene();
    SceneManager.LoadScene(current.name);
    yield return null; // 한 프레임 기다리기 (씬 전환 완료까지 대기)

    // 그 후에 상태 설정
    
    GameManager.Instance.currentGameState = GameManager.GameState.Start;
    Time.timeScale = 0f;
}

    public void GoToMainMenu()
    {
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
}
