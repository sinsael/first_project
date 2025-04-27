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
        PauseUpdate();
    }

    private void PauseUpdate()
    {
        // 게임 일시정지 및 재개 처리
        if (GameManager.Instance.currentGameState == GameManager.GameState.Playing) // 게임 상태가 Playing일 때
        {
            if (Input.GetKeyUp(KeyCode.Escape)) // esc키를 눌렀을 때
            {
                Debug.Log("게임 일시 정지!"); // 디버그 메시지 출력
                GameManager.Instance.currentGameState = GameManager.GameState.Pause; // 게임 상태 변경
                PauseGame(); // 게임 일시 정지
            }
        }
    }

    // 게임 일시 정지 메서드
    void PauseGame()
    {
        Time.timeScale = 0f; // 게임 속도 정지
        GameManager.Instance.currentGameState = GameManager.GameState.Pause; // 게임 상태 변경
        // UI 활성화
        UIManager.Instance.ShowUI(UIManager.UIType.PauseUI); // 일시정지 UI 활성화
    }
}
