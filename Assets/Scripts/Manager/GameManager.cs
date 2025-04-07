using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance {get; set;} // 싱글톤 인스턴스
    // 게임 상태
    public enum GameState
    {
        Start,
        Playing,
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
    }

    void Update()
    {
        
    }

    // 게임 재시작 메서드
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     // 게임 재시작 함수
    public void RestartStage()
    {
        
        SpawnManager.Instance.DeactivateEnemies(); // 적 비활성화
        SpawnManager.Instance.DeactivateLongNotes(); // 롱노트 비활성화
        SpawnManager.Instance.DeactivatePlatforms(); // 플랫폼 비활성화
        SpawnManager.Instance.DeactivatePlayers(); // 플레이어 비활성화
        // 게임 상태 초기화
        SpawnManager.Instance.SpawnEnemies(); // 적 리스폰
        SpawnManager.Instance.SpawnLongNotes(); // 롱노트 리스폰
        SpawnManager.Instance.SpawnPlatforms(); // 플랫폼 리스폰
        SpawnManager.Instance.SpawnPlayers(); // 플레이어 리스폰    
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f; // 게임 재시작
    }

}
