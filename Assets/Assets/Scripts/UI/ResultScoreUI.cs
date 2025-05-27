using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ScoreBool
{
    public bool Score;
    public bool Perfect;
    public bool Nice;
    public bool Miss;
}

public class ResultScoreUI : MonoBehaviour
{
    TextMeshProUGUI Text;
    public bool SetActiveBool;  
    public ScoreBool scoreBool;

    void Start()
    {
        // TextMeshProUGUI 컴포넌트 가져오기
        Text = GetComponent<TextMeshProUGUI>();
        UpdateScoreUI(); // 초기 점수 UI 업데이트
    }

    // 스코어 UI 출력
    void UpdateScoreUI()
    {
        if (scoreBool.Score == true)
        {
            // 점수 출력
            int score = ScoreManager.Instance.currentScore;
            // 점수 UI에 점수 업데이트
            Text.text = "Score : " + score.ToString();
        }
        if (scoreBool.Perfect == true)
        {
            int Perfect = ScoreManager.Instance.currentperfect;
            Text.text = " Perfect : " + Perfect.ToString();
        }
        if (scoreBool.Nice == true)
        {
            int Nice = ScoreManager.Instance.currentNice;
            Text.text = " Nice : " + Nice.ToString();
        }if (scoreBool.Miss == true)
        {
            int Miss = ScoreManager.Instance.currentMiss;
            Text.text = " Miss : " + Miss.ToString();
        }

    }

}
