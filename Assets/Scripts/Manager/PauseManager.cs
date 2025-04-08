using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static PauseManager Instance { get; private set; } // 싱글톤 인스턴스
    void Start()
    {
        if (Instance == null)
        {
            Instance = this; // 인스턴스 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
            return;
        }
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 GameManager 유지
    }

    void Update()
    {
        // 게임 일시정지 및 재개 처리
        if (GameManager.Instance.currentGameState == GameManager.GameState.Playing) // 게임 상태가 Playing일 때
        {
            if (Input.GetKeyUp(KeyCode.P)) // P키를 눌렀을 때
            {
                Debug.Log("게임 일시 정지!"); // 디버그 메시지 출력
                GameManager.Instance.previousGameState = GameManager.Instance.currentGameState; // 이전 게임 상태 저장
                PauseGame(); // 게임 일시 정지
            }
        }
        if (GameManager.Instance.currentGameState == GameManager.GameState.Pause) // 게임 상태가 Pause일 때
        {
            if (Input.GetKeyDown(KeyCode.P)) 
            {
                Debug.Log("게임 재개!"); // 디버그 메시지 출력
                StartCoroutine(ResumeGameAfterDelay(3f)); // 3초 대기 후 게임 재개
            }
        }
    }

    // 게임 일시 정지 메서드
    void PauseGame()
    {
        Time.timeScale = 0f; // 게임 속도 정지
        GameManager.Instance.currentGameState = GameManager.GameState.Pause; // 게임 상태 변경
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



}
