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
        dialogue,
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
        if(currentGameState == GameState.Start)
        {
            if(Input.anyKey)
            {
                Time.timeScale = 1f;
                currentGameState = GameState.Playing;
            }
        }
            // 게임이 진행 중일 때 입력 처리
            switch (currentGameState)
            {
                case GameState.Start:
                    break;
                case GameState.dialogue:
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

    public void startDialogue()
    {
        currentGameState = GameState.dialogue; // 게임 상태 변경
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

    public void GameClear()
    {
        currentGameState = GameState.Clear; // 게임 상태 변경
    }
}
