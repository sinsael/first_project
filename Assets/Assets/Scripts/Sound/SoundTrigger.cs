using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리
public class BGMTrigger : MonoBehaviour
{
    [SerializeField] float volume = 1.0f; // 사운드 볼륨 (0.0f ~ 1.0f)
    [SerializeField] bool playOnAwake = false; // Awake 시 자동 재생 여부
    AudioSource audioSource; // AudioSource 컴포넌트
    bool isPlaying = false; // 사운드 재생 여부

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가
        audioSource.volume = volume; // 볼륨 설정
        audioSource.loop = true; // 반복 재생 설정
    }

    // 오브젝트 트리거 발동 시 사운드 재생
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SoundManager.instance == null) return;

        if (collision.CompareTag("Player") && !isPlaying) // 플레이어와 충돌 시
        {
            SoundManager.instance.PlayBGMForCurrentScene(); // 사운드 재생
            isPlaying = true; // 재생 상태 업데이트
            Destroy(gameObject); // 트리거 오브젝트 파괴
        }
    }


}
