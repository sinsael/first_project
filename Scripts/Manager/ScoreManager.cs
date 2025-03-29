using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int currentScore = 0;
    private int currentperfect = 0;
    private int currentnice = 0;
    public Text text;

    void Start()
    {
        SetText();
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

    public void AddScore(int score)
    {
        currentScore += score; // 점수 증가
        SetText();
    }

    public void ResetScore(int score)
    {
        currentScore = score; // 점수 초기화
        SetText();
    }

    public void ResetPerfect()
    {
        currentperfect = 0; // 퍼펙트 초기화
    }

    public void AddPerfect()
    {
        currentperfect++;
        AddScore(150); // 퍼펙트 점수 추가
    }

    public void AddNice()
    {
        currentnice++;
        AddScore(100); // 나이스 점수 추가
    }

    public void Resetnice()
    {
        currentnice = 0; // 점수 초기화
    }


    public int GetPerfect()
    {

        return currentperfect;
    }


    public int GetNice()
    {

        return currentnice;
    }

    public int GetScore()
    {
        return currentScore;
    }
    public void SetText()
    {
        text.text = "Score : " + currentScore.ToString();
    }

}
