using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI Text;
    [SerializeField] int lastScore = 0;


    void Start()
    {
        // TextMeshProUGUI 컴포넌트 가져오기
        Text = GetComponent<TextMeshProUGUI>();
        UpdateScoreUI(); // 초기 점수 UI 업데이트
    }
    void Update()
    {
        if (lastScore != ScoreManager.Instance.currentScore)
        {
            UpdateScoreUI();
            lastScore = ScoreManager.Instance.currentScore;
        }
        
    }

    // 스코어 UI 출력
    void UpdateScoreUI()
    {
        // 점수 출력
        int score = ScoreManager.Instance.currentScore;
        // 점수 UI에 점수 업데이트
        Text.text = "Score : " + score.ToString();
    }
}
