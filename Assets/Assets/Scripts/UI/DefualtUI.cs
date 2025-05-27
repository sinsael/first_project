using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefualtUI : MonoBehaviour
{
    // 파괴할 스크립트
    [SerializeField] private GameObject[] destroyScripts;
    public virtual void ContinueButton()
    {
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        SFX.Instance.PlaySFX(1); // 효과음 재생
        StartCoroutine(ContinueGameAfterDelay(3f)); // 3초 대기 후 게임 재개
    }

    // 게임 재시작 메서드
    public virtual void RestartStageButton()
    {
        Debug.Log("게임 재시작");
        SFX.Instance.PlaySFX(1); // 효과음 재생
        StartCoroutine(RestartAfterSceneLoad());
    }

    // 3초 대기후 게임 재개 메서드
    public virtual IEnumerator ContinueGameAfterDelay(float delay)
    {
        Debug.Log("게임 재개 대기 중..."); // 디버그 메시지 출력
        yield return new WaitForSecondsRealtime(delay); // 3초 대기
        // 일시정지 화면 투명하게 만들기
        Debug.Log("대기 시간 : " + delay + "초"); // 대기 시간 출력
        Debug.Log("게임 재개!"); // 디버그 메시지 출력
        SoundManager.instance.ResumeSound();
        ContinueGame(); // 게임 재개
    }

    // 게임 재개 메서드
    public virtual void ContinueGame()
    {
        Time.timeScale = 1f; // 게임 속도 정상화
        GameManager.Instance.StartGame();
        /// UI 비활성화
        UIManager.Instance.HideUI(UIManager.UIType.NoneUI); // UI 비활성화
    }


    public virtual IEnumerator RestartAfterSceneLoad()
    {
        GameManager.Instance.OpenGame();
        gameObject.SetActive(false);
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        SoundManager.instance.StopAllSounds();
        yield return null; // 한 프레임 기다리기 (씬 전환 완료까지 대기)
        
    }

    public virtual void GoToMainMenu()
    {

        SFX.Instance.PlaySFX(1); // 효과음 재생
        // 현재 활성/비활성 포함 모든 GameObject 찾기 (Resources.FindObjectsOfTypeAll)
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        GameManager.Instance.OpenGame(); // 게임 상태 변경
        SoundManager.instance.StopAllSounds(); // 모든 사운드 정지
        foreach (GameObject obj in allObjects)
        {
            // 파괴할 스크립트가 있는지 확인
            foreach (GameObject script in destroyScripts)
            {
                if (obj.name == script.name)
                {
                    Destroy(obj); // 해당 GameObject 파괴
                    break;
                }
            }
        }

        // 메인 메뉴 씬으로 이동
        SceneManager.LoadScene("Lobby");
    }
}