using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    [SerializeField] AudioSource soundSource; // 효과음 설정
    [SerializeField] AudioSource bgmSource; // 배경음악 설정
    [SerializeField] public AudioClip[] sfxList; // 사운드 리스트
    [SerializeField] public AudioClip[] bgmList; // 배경음악 리스트

    void Awake()
    {
        if (instance == null) // 인스턴스가 없으면 생성
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else if (instance != this) // 이미 인스턴스가 존재하면 파괴
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        soundSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가
        bgmSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가
        bgmSource.loop = false; // 배경음악 반복 재생 설정
    }

    // soundSource의 볼륨을 설정하는 프로퍼티
    public float SoundVolume
    {
        get { return soundSource.volume; }
        set { soundSource.volume = value; }
    }

    // 배경음악을 재생하는 메서드
    public void PlayBGM(int soundIndex)
    {
        if (soundIndex < 0 || soundIndex >= bgmList.Length) // 인덱스 범위 체크
        {
            Debug.LogWarning("Invalid BGM index: " + soundIndex); // 경고 메시지 출력
            return; // 메서드 종료
        }
        bgmSource.clip = bgmList[soundIndex]; // 배경음악 설정
        bgmSource.Play(); // 배경음악 재생
    }

    public void PauseSound()
    {
        soundSource.Pause(); // 효과음 일시 정지
        bgmSource.Pause(); // 배경음악 일시 정지
    }

    public void ResumeSound()
    {
        soundSource.UnPause(); // 효과음 재개
        bgmSource.UnPause(); // 배경음악 재개
    }


    public void StopAllSounds()
    {
        soundSource.Stop();
        bgmSource.Stop(); // 모든 사운드 정지
        ResetSFXSound();
        ResetBGMSound();
    }

    // 클리어 사운드 재생 메서드
    public void PlayClearSound()
    {
        soundSource.PlayOneShot(sfxList[0]); // 클리어 사운드 재생
    }

    public void ResetSFXSound()
    {
        soundSource.clip = null;
    }

    public void ResetBGMSound()
    {
        bgmSource.clip = null;
    }
}
