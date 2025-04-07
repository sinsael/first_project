using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; } // 싱글톤 인스턴스
    private bool isGamePaused = false; // 게임 일시 정지 여부
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
        // Time.timeScale이 0이어도 입력 받기
        if (Time.timeScale == 0f)
        {
            // 게임 시작 상태일 때만 입력 받기
            if (currentGameState == GameState.Start && Input.anyKeyDown)
            {
                currentGameState = GameState.Playing;
                Time.timeScale = 1f;  // 게임 속도 정상화
            }
            else if (isGamePaused == true && currentGameState == GameState.Pause && Input.anyKeyDown)
            {
                // 3초 대기 후 게임 재개
                StartCoroutine(ResumeGameAfterDelay(3f)); // 3초 대기 후 게임 재개
                isGamePaused = false; // 게임 일시 정지 상태 해제
            }

        }
        else
        {
            // 게임이 진행 중일 때 입력 처리
            switch (currentGameState)
            {
                case GameState.Start:
                    // 이 부분은 더 이상 필요 없음
                    break;

                case GameState.Playing:
                    if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 1f)
                    {
                        // 게임 일시 정지
                        previousGameState = currentGameState;
                        currentGameState = GameState.Pause;
                        Time.timeScale = 0f; // 게임 속도 정지
                        Debug.Log("게임 일시 정지!");
                        isGamePaused = true; // 게임 일시 정지 상태 설정
                    }
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        RestartScene();
                    }

                    break;
                case GameState.Clear:
                    break;
            }
        }
    }



    // 게임 시작 메서드
    public void StartGame()
    {
        currentGameState = GameState.Playing; // 게임 상태 변경
    }

    private IEnumerator ResumeGameAfterDelay(float delay)
    {
        Debug.Log("게임 재개 대기 중...");
        yield return new WaitForSecondsRealtime(delay); // 3초 대기
        for (int i = 0; i < delay; i++)
        {
            Debug.Log("대기 중: " + (delay - i) + "초 남음");
            yield return new WaitForSecondsRealtime(1f); // 1초 대기
        }
        currentGameState = GameState.Playing; // 게임 상태 변경
        Time.timeScale = 1f; // 게임 속도 정상화
        Debug.Log("게임 재개!");
        delay = 0f; // 대기 시간 초기화
    }

    public void GameOver()
    {
        currentGameState = GameState.GameOver; // 게임 상태 변경
        // 게임 오버 UI 활성화
        Time.timeScale = 0f; // 게임 속도 정지
    }

    // 게임 클리어 메서드
    public void GameClear()
    {
        currentGameState = GameState.Clear; // 게임 상태 변경
        // 게임 클리어 UI 활성화
        Time.timeScale = 0f; // 게임 속도 정지
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

    // 씬 재시작
    public void RestartScene()
    {
        // 현재 씬을 다시 로드하여 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // 게임 상태 초기화
        currentGameState = GameState.Start; // 게임 상태 초기화
    }
}
