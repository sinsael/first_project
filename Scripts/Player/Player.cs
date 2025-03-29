using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float maxspeed;
    public float jumpForce = 5f; // 점프력
    bool isJumping = false;
    public GameObject prefab;
    Rigidbody2D rigid;
    Animator anim;
    public BPMManager bpmManager;
    public JudgementLine judgementLine;  // JudgementLine을 참조
    public ScoreManager scoreManager;
    void Awake()
    {
        scoreManager = ScoreManager.Instance;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어 자동 이동
        transform.position = new Vector3(transform.position.x + maxspeed * Time.deltaTime, transform.position.y, transform.position.z);
        anim.SetBool("isrun", true);

        // 점프 키 입력
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
        {
            Jump();
        }
    }

    // 점프 함수
    void Jump()
    {
        if (!isJumping)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, jumpForce);
            anim.SetBool("isrun", false);
            anim.SetBool("isJump", true);
            isJumping = true;
        }
    }

    // 플레이어가 땅에 닿았을 때 실행되는 함수
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigid.linearVelocity.y <= 0)
        {
            isJumping = false;
            anim.SetBool("isJump", false);
        }
    }

    // 플레이어가 적과 부딪혔을 때 실행되는 함수
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Debug.Log("플레이어가 적과 부딪혔습니다.");
            scoreManager.ResetScore(0);
        }
    }
}
