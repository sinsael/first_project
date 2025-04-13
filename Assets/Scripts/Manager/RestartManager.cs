using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 네임스페이스 추가


public class RestartManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static RestartManager Instance { get; private set; } // 싱글톤 인스턴스
    private readonly List<GameManager.GameState> restartgameStates = new List<GameManager.GameState>()
  {
    GameManager.GameState.Pause,
    GameManager.GameState.GameOver
};


    void Start()
    {
        if (Instance == null)
        {
            Instance = this; // 인스턴스 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복된 객체 젝
            return;
        }
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 GameManager 유지
    }

    public void Update()
    {
        RestartUpdate();
    }

    private void RestartUpdate()
    {
        // 게임이 진행 중일때 입력 처리
        if (restartgameStates.Contains(GameManager.Instance.currentGameState)) // 게임 상태가 Playing일 때
        {
            if (Input.GetKeyUp(KeyCode.R)) // R키를 눌렀을 때
            {
                Debug.Log("게임 재시작!"); // 디버그 메시지 출력
                RestartStage(); // 게임 재시작
            }
        }
    }

    // 게임 재시작 메서드
    public void RestartStage()
    {
        // 현재 씬을 다시 로드하여 게임 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 다시 로드
        GameManager.Instance.currentGameState = GameManager.GameState.Start; // 게임 상태 초기화
        Time.timeScale = 0f; // 게임 속도 정상화
    }


}
