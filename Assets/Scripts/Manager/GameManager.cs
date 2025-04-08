using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스
    // 게임 상태
    public enum GameState
    {
        Start,
        Playing,
        Pause,
        GameOver,
        Clear
    }
    public GameState currentGameState = GameState.Start; // 현재 게임 상태
    public GameState previousGameState = GameState.Start; // 이전 게임 상태




    // 게임 시작 시 호출되는 메서드
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

        // 게임 시작 시 초기 상태 설정
        currentGameState = GameState.Start; // 게임 상태 초기화
        Time.timeScale = 0f; // 게임 속도 정지
    }

    void Update()
    {
            // 게임이 진행 중일 때 입력 처리
            switch (currentGameState)
            {
                case GameState.Start:
                    break;
                case GameState.Playing:
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Clear:
                    break;
            }
    }



    // 게임 시작 메서드
    public void StartGame()
    {
        currentGameState = GameState.Playing; // 게임 상태 변경
    }

    public void ResumeGame()
    {
        currentGameState = GameState.Playing; // 게임 상태 변경
    }

    public void PauseGame()
    {
        currentGameState = GameState.Pause; // 게임 상태 변경
    }

    public void GameOver()
    {
        currentGameState = GameState.GameOver; // 게임 상태 변경
    }

    // 게임 클리어 메서드
    public void GameClear()
    {
        currentGameState = GameState.Clear; // 게임 상태 변경
    }

    // 다음 씬으로 넘어가는 메서드
    public void NextScene()
    {
        // 다음 씬으로 넘어가는 로직 구현
        // 예를 들어, 다음 씬의 빌드 인덱스가 현재 씬의 빌드 인덱스 + 1이라고 가정
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // 다음 씬 로드
        }
        else
        {
            Debug.Log("마지막 씬입니다."); // 마지막 씬일 경우 메시지 출력
        }

    }
}
