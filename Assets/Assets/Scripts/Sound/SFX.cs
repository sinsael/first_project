using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFX : MonoBehaviour
{
    public static SFX Instance { get; private set; }
    SoundManager soundManager; // SoundManager 인스턴스

    void Awake()
    {
        soundManager = SoundManager.instance; // SoundManager 인스턴스 가져오기
        if (Instance != null)
        {
            Destroy(gameObject); // 중복 제거
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
    }

    void Update()
{
    if (soundManager == null && SoundManager.instance != null)
    {
        soundManager = SoundManager.instance;
    }
}


    // 효과음을 재생하는 메서드
    public void PlaySFX(int soundIndex)
    {
        if (soundManager == null) return; // SoundManager가 없으면 종료
        soundManager.PlaySFX(soundIndex); // 효과음 재생
    }
    // 효과음을 정지하는 메서드
    public void StopSFX()
    {
        if (soundManager == null) return; // SoundManager가 없으면 종료
        soundManager.StopSFX(); // 효과음 정지
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void CreateSFXIfNotExists()
    {
        if (Instance == null)
        {
            GameObject sfxGO = new GameObject("SFX");
            sfxGO.AddComponent<SFX>();
        }
    }

}
