using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    [SerializeField] private AudioSource soundSource; // 사운드 설정
    [SerializeField] private AudioSource bgmSource; // BGM 설정
    [SerializeField] private AudioClip[] sfxList; // 사운드 리스트
    
}
