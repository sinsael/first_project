using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [Tooltip("점수 매니저")]
    public static ScoreManager Instance { get;  set; }
    [Tooltip("점수")]
    private int currentScore = 0;
    [Tooltip("퍼펙트 점수")]
    private int currentperfect = 0;
    [Tooltip("나이스 점수")]
    private int currentNice = 0;
    [Tooltip("미스 점수")]
    private int currentMiss = 0;

    void Start()
    {
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
// 아시 발발
    public void AddScore(int score)
    {
        currentScore += score;
    }

    public void AddPerfect(int score)
    {
        currentperfect++;
        AddScore(score); // 퍼펙트 점수 추가
    }

    public void AddNice(int score)
    {
        currentNice++;
        AddScore(score); // 나이스 점수 추가
    }
    
    public void AddMiss()
    {
        currentMiss++;
    }

    public int GetPerfect()
    {

        return currentperfect;
    }


    public int GetNice()
    {

        return currentNice;
    }

    public int GetScore()
    {
        return currentScore;
    }
    public int GetMiss()
    {
        return currentMiss;
    }

    public void ResetScore()
    {
        currentScore = 0;
        currentperfect = 0;
        currentNice = 0;
    }

    public void ResetMiss()
    {
        currentMiss = 0;
    }
}
