using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUI : DefualtUI
{
    [SerializeField] string stagename;
     public void nextStageButton()
    {

        Debug.Log("다음 스테이지지로 이동");

        GameManager.Instance.OpenGame();

        Time.timeScale = 1f;

        SceneManager.LoadScene(stagename); // 다음 스테이지로 이동
    }
}