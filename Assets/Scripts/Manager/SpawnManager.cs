using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [Header("적 스폰 위치")]
    [Tooltip("적 스폰 위치")]
    private Vector3[] originalEnemyPositions;
    private GameObject[] enemiesInScene;
    [Header("롱노트 스폰 위치")]
    [Tooltip("롱노트 스폰 위치")]
    private Vector3[] originalLongNotePositions;
    private GameObject[] longNotesInScene;
    public static SpawnManager Instance {get; set;} // 싱글톤 인스턴스
    [Header("플랫폼 스폰 위치")]
    [Tooltip("플랫폼 스폰 위치")]
    private Vector3[] originalPlatformPositions;
    private GameObject[] platformsInScene;
    [Header("플레이어 스폰 위치")]
    [Tooltip("플레이어 스폰 위치")]
    private Vector3[] originalPlayerPositions;
    private GameObject[] playersInScene;
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

    DontDestroyOnLoad(gameObject); // 씬 전환 시에도 SpawnManager 유지

    // 적 스폰 위치를 미리 배치된 적들로 설정
    enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy"); // 미리 배치된 적들

    originalEnemyPositions = new Vector3[enemiesInScene.Length];
    for (int i = 0; i < enemiesInScene.Length; i++)
    {
        originalEnemyPositions[i] = enemiesInScene[i].transform.position;
    }

    // 롱노트 스폰 위치를 미리 배치된 롱노트들로 설정
    longNotesInScene = GameObject.FindGameObjectsWithTag("LongnoteEnemy"); // 미리 배치된 롱노트들

    originalLongNotePositions = new Vector3[longNotesInScene.Length];
    for (int i = 0; i < longNotesInScene.Length; i++)
    {
        originalLongNotePositions[i] = longNotesInScene[i].transform.position;
    }

    // 플랫폼 스폰 위치를 미리 배치된 플랫폼들로 설정
    platformsInScene = GameObject.FindGameObjectsWithTag("Ground"); // 미리 배치된 플랫폼들
    originalPlatformPositions = new Vector3[platformsInScene.Length];
    for (int i = 0; i < platformsInScene.Length; i++)
    {
        originalPlatformPositions[i] = platformsInScene[i].transform.position;
    }

    // 플레이어 스폰 위치를 미리 배치된 플레이어들로 설정
    playersInScene = GameObject.FindGameObjectsWithTag("Player"); // 미리 배치된 플레이어들
    originalPlayerPositions = new Vector3[playersInScene.Length];
    for (int i = 0; i < playersInScene.Length; i++)
    {
        originalPlayerPositions[i] = playersInScene[i].transform.position;
    }
}


    public void SpawnEnemies()
    {
        for (int i = 0; i < enemiesInScene.Length; i++)
        {
            enemiesInScene[i].transform.position = originalEnemyPositions[i]; // 원래 위치로 되돌리기
            enemiesInScene[i].SetActive(true); // 적 활성화
        }
    }

    public void DeactivateEnemies()
    {
        for (int i = 0; i < enemiesInScene.Length; i++)
        {
            enemiesInScene[i].SetActive(false); // 적 비활성화
        }
    }
    public void SpawnLongNotes()
    {
        for (int i = 0; i < longNotesInScene.Length; i++)
        {
            longNotesInScene[i].transform.position = originalLongNotePositions[i]; // 원래 위치로 되돌리기
            longNotesInScene[i].SetActive(true); // 롱노트 활성화
        }
    }
    public void DeactivateLongNotes()
    {
        for (int i = 0; i < longNotesInScene.Length; i++)
        {
            longNotesInScene[i].SetActive(false); // 롱노트 비활성화
        }
    }

    public void SpawnPlatforms()
    {
        for (int i = 0; i < platformsInScene.Length; i++)
        {
            platformsInScene[i].transform.position = originalPlatformPositions[i]; // 원래 위치로 되돌리기
            platformsInScene[i].SetActive(true); // 플랫폼 활성화
        }
    }
    public void DeactivatePlatforms()
    {
        for (int i = 0; i < platformsInScene.Length; i++)
        {
            platformsInScene[i].SetActive(false); // 플랫폼 비활성화
        }
    }
    public void SpawnPlayers()
    {
        for (int i = 0; i < playersInScene.Length; i++)
        {
            playersInScene[i].transform.position = originalPlayerPositions[i]; // 원래 위치로 되돌리기
            playersInScene[i].SetActive(true); // 플레이어 활성화
        }
    }
    public void DeactivatePlayers()
    {
        for (int i = 0; i < playersInScene.Length; i++)
        {
            playersInScene[i].SetActive(false); // 플레이어 비활성화
        }
    }

}
