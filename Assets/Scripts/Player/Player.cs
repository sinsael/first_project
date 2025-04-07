using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("플레이어 스탯")]
    [Tooltip("플레이어 속도")]
    public float maxspeed;
    [Tooltip("플레이어 점프력")]
    public float jumpForce = 5f; // 점프력
    bool isJumping = false;
    [Header("점프 관련")]
    public KeyCode[] jumpKey = { KeyCode.Space, KeyCode.F, KeyCode.D }; // 점프 키 배열
    Rigidbody2D rigid;
    Animator anim;
    private string[] enemyTag = { "Enemy", "LongnoteEnemy", "trap" }; // 적 태그 배열

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어 자동 이동
        transform.position = new Vector3(transform.position.x + maxspeed * Time.deltaTime, transform.position.y, transform.position.z);

        // 점프 키 입력 체크
        if (jumpKey.Any(key => Input.GetKeyDown(key)) && !isJumping)
        {
            // 점프 함수 호출
            Jump();
        }
    }

    // 점프 함수
    void Jump()
    {
        if (!isJumping)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // 땅에 닿으면 점프 상태 해제
            Debug.Log("플레이어가 땅에 닿았습니다.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyTag.Contains(collision.tag))
        {
            // 적과 충돌 시 사망 처리
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {   
        gameObject.SetActive(false); // 플레이어 비활성화
        // 게임 잠시 멈춤
        Time.timeScale = 0f; // 게임 일시 정지
        yield return new WaitForSecondsRealtime(1f); // 1초 대기

        Debug.Log("플레이어가 사망했습니다.");
    }
}
