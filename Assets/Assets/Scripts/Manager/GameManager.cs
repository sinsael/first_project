using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class GameData
    {
#if UNITY_EDITOR
        public SceneAsset sceneAsset; // 씬 에셋
#endif
        public string sceneName; // 씬 이름
    }

    public List<GameData> gameDataList = new List<GameData>(); // 게임 데이터 리스트

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
    public GameState currentGameState; // 현재 게임 상태




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
        OpenGame(); // 게임 시작 메서드 호출

        Time.timeScale = 0f; // 게임 속도 0으로 설정 (일시정지 상태)

    }

    void Update()
    {

        GameStartMethod();
        // 게임이 진행 중일 때 입력 처리
        SwitchGameState();
    }

    private void SwitchGameState()
    {
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

    private void GameStartMethod()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; // 현재 씬 이름 가져오기

        if (currentGameState != GameState.Start) // 게임 상태가 Start가 아닐 때
        {
            return; // 메서드 종료
        }

        foreach (GameData gameData in gameDataList)
        {
            if (currentGameState == GameState.Start && gameData.sceneName == currentSceneName) // 게임 상태가 Start이고 현재 씬이 첫 번째 스테이지일 때
            {
                if (Input.anyKey)
                {
                    Debug.Log("게임 시작!"); // 디버그 메시지 출력
                    StartGame(); // 게임 시작 메서드 호출
                }
                break;
            }
        }

    }



    // 게임 시작 메서드
    public void OpenGame()
    {
        currentGameState = GameState.Start; // 게임 상태 변경
        Time.timeScale = 0f; // 게임 속도 0으로 설정 (일시정지 상태)
    }
    public void StartGame()
    {
        currentGameState = GameState.Playing; // 게임 상태 변경
        Time.timeScale = 1f; // 게임 속도 정상화
    }

    public void ResumeGame()
    {
        currentGameState = GameState.Playing; // 게임 상태 변경

    }

    public void PauseGame()
    {
        currentGameState = GameState.Pause; // 게임 상태 변경
        UIManager.Instance.ShowUI(UIManager.UIType.PauseUI); // 일시정지 UI 활성화
    }

    public void GameOver()
    {
        currentGameState = GameState.GameOver; // 게임 상태 변경
        UIManager.Instance.ShowUI(UIManager.UIType.GameOverUI);
        ScoreManager.Instance.AddMiss(); // 게임 오버 시 점수 처리
        SoundManager.instance.StopAllSounds();
        Time.timeScale = 0f; // 게임 속도 0으로 설정 (일시정지 상태)
    }

    public void GameClear()
    {
        currentGameState = GameState.Clear; // 게임 상태 변경
        UIManager.Instance.ShowUI(UIManager.UIType.ClearUI);
        Time.timeScale = 0f; // 게임 속도 0으로 설정 (일시정지 상태)
    }
}
