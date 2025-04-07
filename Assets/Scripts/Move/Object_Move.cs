using UnityEngine;

public class Object_Move : MonoBehaviour
{
    // 이동하지 않는 상태 묶는 키워드
    void FixedUpdate()
    {
        // 게임 playing 상태일떄만 이동
        if (GameManager.Instance.currentGameState == GameManager.GameState.Playing)
        {
            // Time.deltaTime을 사용하여 이동 속도 조절
            transform.position += Vector3.left * Time.deltaTime * 5f; // 오른쪽으로 이동
        }
        // 게임이 정지 상태일 때는 이동하지 않음
        else if (GameManager.Instance.currentGameState == GameManager.GameState.Start)
        {
            // 게임 시작 상태일 때는 이동하지 않음
            return;
        }
        else if (GameManager.Instance.currentGameState == GameManager.GameState.Pause)
        {
            // 게임이 일시정지 상태일 때는 이동하지 않음
            return;
        }
        else if (GameManager.Instance.currentGameState == GameManager.GameState.GameOver)
        {
            // 게임 오버 상태일 때는 이동하지 않음
            return;
        }
        else if (GameManager.Instance.currentGameState == GameManager.GameState.Clear)
        {
            // 게임 클리어 상태일 때는 이동하지 않음
            return;
        }
    }
}
