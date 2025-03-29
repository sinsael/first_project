using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class JudgementLine : MonoBehaviour
{
    public float judgeWindow = 0.5f;  // 판정 타이밍 범위 (조정된 값)
    public Transform player; // 플레이어 캐릭터 위치치
    public Vector3 offset; // 캐릭터와 판정선의 거리

    enum JudgeResult { Perfect, Nice, None } // 판정 종류
    private JudgeResult currentJudge = JudgeResult.None;  // 기본값을 None으로 설정

    bool isAttack = false; // 공격 여부

    ScoreManager scoreManager; // 스코어매니저 함수 접근 코드드

    void Start()
    {
        scoreManager = ScoreManager.Instance; // 스크립트 함수 접근하기
    }

    void Update()
    {
        // 캐릭터와 판정선의 위치 
        if (player != null)
        {
            transform.position = player.position + offset;
        }
        //공격
        if (Input.GetKey(KeyCode.J) && !isAttack)
        {
            attack();
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            isAttack = false; // 공격 상태 종료
        }

    }

    void attack()
    {
        isAttack = true;

        // "Enemy" 태그를 가진 모든 오브젝트 판정
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObj in enemies)
        {
            float distance = math.abs(transform.position.x - enemyObj.transform.position.x); // 공격 판정선과 적의 x 위치 비교

            if (distance <= judgeWindow) // 판정선 범위 내의 적들만 체크
            {
                if (distance <= judgeWindow * 0.2f) // 20% 범위 내: perfect
                {
                    currentJudge = JudgeResult.Perfect;
                }
                else if (distance <= judgeWindow * 0.5f) // 50% 범위 내 : Nice
                {
                    currentJudge = JudgeResult.Nice;
                }

                // none이 아닐 경우 공격 시작작
                if (currentJudge != JudgeResult.None)
                {
                    ApplyDamage(enemyObj.GetComponent<Collider2D>());
                }

                if (currentJudge == JudgeResult.Perfect)
                {
                    scoreManager.AddPerfect();
                }
                else if (currentJudge == JudgeResult.Nice)
                {
                    scoreManager.AddNice();
                }
            }
        }
    }

    void ApplyDamage(Collider2D collision)
    {
        // 적에게 데미지를 적용하는 로직 추가
        if (collision.CompareTag("Enemy")) // 적과 충돌
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1, transform.position);  // 데미지 1 적용
            }
        }
    }
}
