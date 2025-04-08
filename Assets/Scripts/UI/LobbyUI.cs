using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
   // 로비 UI를 관리하는 클래스
   public Button startButton; // 시작 버튼
   public Button howToPlayButton; // 조작법 버튼
   public Button exitButton; // 종료 버튼

    void Awake()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>(); // 시작 버튼 찾기
        howToPlayButton = GameObject.Find("HowToPlayButton").GetComponent<Button>(); // 조작법 버튼 찾기
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>(); // 종료 버튼 찾기
    }
    public void Start()
   {
       startButton.onClick.AddListener(OnStartButtonClicked); // 시작 버튼 클릭 시 호출될 메서드 등록
       howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked); // 조작법 버튼 클릭 시 호출될 메서드 등록
         exitButton.onClick.AddListener(OnExitButtonClicked); // 종료 버튼 클릭 시 호출될 메서드 등록
   }

    // 시작 버튼 클릭 시 호출되는 메서드
    public void OnStartButtonClicked()
    {
        // 게임 시작 씬으로 전환하는 코드 
         UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); // 게임 씬으로 전환
    }

    // 조작법 버튼 클릭 시 호출되는 메서드
    public void OnHowToPlayButtonClicked()
    {
        // 조작법 UI를 활성화하는 코드 작성
        Debug.Log("조작법 버튼 클릭됨");
    }

    // 종료 버튼 클릭 시 호출되는 메서드
    public void OnExitButtonClicked()
    {
        // 애플리케이션 종료
        Application.Quit();
        Debug.Log("게임 종료됨");
    }

}
