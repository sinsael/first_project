using System;
using System.Collections;
using UnityEngine;

public class LongnoteEnemy : MonoBehaviour
{
    [SerializeField] private float followSpeed = 5f; // 따라가는 속도
    [SerializeField] private float scoreInterval = 0.5f; // 점수 추가 간격
    private const int perfectScore = 200; // 롱노트 적 공격 시 추가되는 점수
    private Transform followTarget;
    private ScoreManager scoreManager; // 스코어 매니저 함수 접근 코드
    private event Action<int> LongnoteHit; // 롱노트 적 공격 시 호출되는 델리게이트
    private bool isFollowing = false;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            LongnoteHit += scoreManager.AddPerfect;
        }
        else
        {
            Debug.LogWarning("ScoreManager를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        FollowJudgement();
    }

    public void StartFollowing()
    {
        if (!isFollowing) // 중복 실행 방지
        {
            followTarget = GameObject.FindGameObjectWithTag("JudgementLine").transform;
            isFollowing = true;
            StartCoroutine(ScoreWhileHolding());
        }
    }

    private void FollowJudgement()
    {
        if (isFollowing && followTarget != null)
        {
            // 적이 판정선의 X 위치를 따라감
            float newX = Mathf.MoveTowards(transform.position.x, followTarget.position.x, followSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    public void StopFollowing()
    {
        if (isFollowing) // 중복 실행 방지
        {
            isFollowing = false;
            StopAllCoroutines(); // 코루틴 정지
        }
    }

    IEnumerator ScoreWhileHolding()
    {
        while (isFollowing)
        {
            LongnoteHit?.Invoke(perfectScore); // 롱노트를 누르고 있으면 지속적으로 점수 추가
            yield return new WaitForSeconds(scoreInterval); // 일정 간격마다 점수 추가
        }
    }

    // 엔드라인에 닿으면 비활성화화
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LongnoteEndline"))
        {
            // **Collider 비활성화**
            GetComponent<Collider2D>().enabled = false;
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        LongnoteHit -= scoreManager.AddPerfect; // 스코어 매니저에 델리게이트 제거  }
    }
}
