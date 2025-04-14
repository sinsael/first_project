using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using TMPro;

public class Player : MonoBehaviour

{
    [Header("플레이어 스탯")]
    [SerializeField] float jumpForce = 5f; // 점프력
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
        transform.position = new Vector3(transform.position.x * Time.deltaTime * Time.timeScale * 0, transform.position.y, transform.position.z);

        Jump();

    }

    // 점프 함수
    void Jump()
    {
        if (!isJumping)
        {
            if (Input.GetKeyDown(jumpKey[0]) || Input.GetKeyDown(jumpKey[1]) || Input.GetKeyDown(jumpKey[2]))
            {

                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                isJumping = true;
            }
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
        if (enemyTag.Contains(collision.tag))
        {
            // 적과 충돌 시 사망 처리
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false); // 플레이어 비활성화
        GameManager.Instance.GameOver(); // 게임 오버 처리
        Debug.Log("플레이어가 사망했습니다.");
    }
}

