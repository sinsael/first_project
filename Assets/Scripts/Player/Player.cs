using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using TMPro;

public class Player : MonoBehaviour

{
    [SerializeField] float jumpForce = 5f; // 점프력
    private string[] enemyTag = { "Enemy", "LongnoteEnemy", "trap" }; // 적 태그 배열
    bool isJumping = false;
    Rigidbody2D rigid;
    Animator anim;
    public KeyCode[] jumpKey = { KeyCode.Space, KeyCode.F, KeyCode.D }; // 점프 키 배열
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();
    }

    // 플레이어 가만히 있는 함수
    private void Idle()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.Start)
        {
            // 플레이어 가만히 있는 상태
            anim.SetBool("ToIdle", true);
            anim.SetBool("ToRun", false);
            anim.SetBool("ToJump", false);
        }
        else
        {
            // 플레이어 가만히 있는 상태
            anim.SetBool("ToIdle", false);
            anim.SetBool("ToRun", true);
            anim.SetBool("ToJump", false);
        }

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

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Clear();
        }
    }

    void Clear()
    {
        GameManager.Instance.GameClear();

        UIManager.Instance.ShowUI(UIManager.UIType.ClearUI);
        Debug.Log("클리어");
    }

    void Die()
    {
        gameObject.SetActive(false); // 플레이어 비활성화
        GameManager.Instance.GameOver(); // 게임 오버 처리
        // 사망 UI 표시시
        Debug.Log("플레이어가 사망했습니다.");
    }
}
