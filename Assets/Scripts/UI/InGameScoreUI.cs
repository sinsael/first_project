using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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
        UpdateScoreUI();
        HideScoreUI();
    }

    // 스코어 UI 출력
    void UpdateScoreUI()
    {
        if (lastScore != ScoreManager.Instance.currentScore)
        {
            int score = ScoreManager.Instance.currentScore;
            Text.text = "Score : " + score.ToString();
            lastScore = ScoreManager.Instance.currentScore;
        }
    }

    void HideScoreUI()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.Clear)
        {
            gameObject.SetActive(false);
        }
    }
}
