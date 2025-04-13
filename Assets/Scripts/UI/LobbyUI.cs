using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{

    // 시작 버튼 클릭 시 호출되는 메서드
    public void StartButton()
    {
        // 게임 시작 씬으로 전환하는 코드 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // 게임 씬으로 전환
    }

    // 조작법 버튼 클릭 시 호출되는 메서드
    public void HowToPlay()
    {
        // 조작법 UI를 활성화하는 코드 작성
        Debug.Log("조작법 버튼 클릭됨");
    }

    // 종료 버튼 클릭 시 호출되는 메서드
    public void QuitButton()
    {
        // 애플리케이션 종료
        Application.Quit();
        Debug.Log("게임 종료됨");
    }

}
