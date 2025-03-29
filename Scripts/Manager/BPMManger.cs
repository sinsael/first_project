using UnityEngine;
using System;

public class BPMManager : MonoBehaviour
{

    public static BPMManager Instance { get; private set; }
    public float bpm = 120f;  // BPM 설정
    private float beatInterval;  // 박자 간격
    private float nextBeatTime;

    public static event Action OnBeat;  // 박자마다 실행될 이벤트

    void Start()
    {
        if (bpm > 0)
        {
            beatInterval = 60f / bpm;
            nextBeatTime = Time.time + beatInterval;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 매니저 유지
        }
        else
        {
            Destroy(gameObject); // 기존 매니저 객체가 있으면 삭제
        }
    }

    void Update()
    {
        if (bpm == 0) return;

        if (Time.time >= nextBeatTime)
        {
            nextBeatTime += beatInterval;
            OnBeat?.Invoke();  // 박자마다 실행될 이벤트 발생
        }
    }

    public float GetBPM()
    {
        return bpm;
    }
}
