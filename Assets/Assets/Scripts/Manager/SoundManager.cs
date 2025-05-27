using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    public AudioSource soundSource; // 효과음 설정
    public AudioSource bgmSource; // 배경음악 설정
    public AudioClip[] soundList; // 사운드 리스트
    public AudioClip[] bgmList; // 배경음악 리스트
    public int soundIndex; // 현재 사운드 인덱스

    private List<AudioSource> sfxSources = new List<AudioSource>(); // 효과음 소스 리스트
    private const int maxSFXSources = 10; // 최대 효과음 소스 개수
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

        foreach (AudioClip clip in soundList) // 효과음 리스트 초기화
        {
            if (clip != null)
            {
                clip.LoadAudioData(); // 오디오 데이터 로드
            }
        }

        foreach (AudioClip clip in bgmList) // 배경음악 리스트 초기화
        {
            if (clip != null)
            {
                clip.LoadAudioData(); // 오디오 데이터 로드
            }
        }

    }

    void Start()
    {
        if (soundSource == null)
            soundSource = gameObject.AddComponent<AudioSource>(); // 자동 생성

        for (int i = 0; i < maxSFXSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(source);
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = false;
    }

    // soundSource의 볼륨을 설정하는 프로퍼티
    public float SoundVolume
    {
        get { return soundSource.volume; }
        set { soundSource.volume = value; }
    }

    public void PlayBGMForCurrentScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // 현재 씬의 인덱스 가져오기
        PlayBGM(sceneIndex); // 인덱스로 BGM 재생
    }

    // 효과음을 재생하는 메서드
    virtual public void PlaySFX(int soundIndex)
    {
        if (soundIndex < 0 || soundIndex >= soundList.Length) // 인덱스 범위 체크
        {
            Debug.LogWarning("Invalid Sfx index: " + soundIndex); // 경고 메시지 출력
            return; // 메서드 종료
        }
        soundSource.loop = false; // 효과음은 반복 재생하지 않음
        soundSource.clip = soundList[soundIndex]; // 효과음 설정
        soundSource.Play(); // 효과음 재생
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

    virtual public void PauseSound()
    {
        foreach (var source in sfxSources)
        {
            source.Pause();
            source.clip = null;
        }

        if (bgmSource != null)
        {
            bgmSource.Pause();
            bgmSource.clip = null;
        }
    }

    virtual public void ResumeSound()
    {
        foreach (var source in sfxSources)
        {
            source.UnPause();
            source.clip = null;
        }

        if (bgmSource != null)
        {
            bgmSource.UnPause();
            bgmSource.clip = null;
        }
    }

    // 효과음을 정지하는 메서드
    virtual public void StopSFX()
    {
        soundSource.Stop(); // 효과음 정지
        ResetSFXSound();
    }

    virtual public void StopAllSounds()
    {
        foreach (var source in sfxSources)
        {
            source.Stop();
            source.clip = null;
        }

        if (bgmSource != null)
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }
    }

    virtual public void ResetSFXSound()
    {
        foreach (var source in sfxSources)
        {
            source.clip = null;
        }
    }

    virtual public void ResetBGMSound()
    {
        bgmSource.clip = null;
    }
}
