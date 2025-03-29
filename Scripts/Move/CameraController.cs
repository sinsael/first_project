using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;   // 카메라 속도
    public GameObject player;          // 플레이어 오브젝트
    public float xOffset = 5.0f;      // 카메라가 플레이어의 왼쪽에 위치하도록 하는 x 오프셋
    public float yOffset = 0.0f;       // y 오프셋, 기본적으로 0이면 플레이어와 동일한 높이에 위치

    private void Update()
    {
        // 카메라의 위치를 플레이어 위치를 기준으로 고정된 오프셋으로 설정
        Vector3 targetPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
        
        // 카메라를 플레이어를 따라가도록 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
