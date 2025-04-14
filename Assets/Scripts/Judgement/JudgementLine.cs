using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class JudgementLine : MonoBehaviour
{
    [Header("판정선 설정")]
    [SerializeField] float judgeWindow = 0.5f;  // 판정 타이밍 범위 (조정된 값)
    [SerializeField] Transform player; // 플레이어 캐릭터 위치
    [SerializeField] Vector3 offset; // 캐릭터와 판정선의 거리
    enum JudgeResult { Perfect, Nice, None } // 판정 종류
    private JudgeResult currentJudge = JudgeResult.None;  // 기본값을 None으로 설정
    [Header("공격 판정")]
    bool isAttack = false; // 공격 여부
    private GameObject currentLongnote = null; // 현재 활성화된 롱노트 적
    [Header("각종 스크립트")]
    [Tooltip("스코어 매니저")]
    ScoreManager scoreManager; // 스코어매니저 함수 접근 코드
    [Tooltip("적 공격 시 호출되는 델리게이트")]
    private static Action<int> NoteHitPerfect; // 적 공격 시 호출되는 델리게이트
    private static Action<int> NoteHitNice;
    [Tooltip("롱노트, 적 공격 시 추가되는 점수")]
    private const int perfectScore = 200; // 롱노트, 적 공격 시 추가되는 점수
    private const int niceScore = 100; // 롱노트, 적 공격 시 추가되는 점수수
    void Start()
    {
        scoreManager = ScoreManager.Instance; // 스코어 매니저 접근
        NoteHitPerfect += scoreManager.AddPerfect; // 스코어 매니저에 델리게이트 추가
        NoteHitNice += scoreManager.AddNice; // 스코어 매니저에 델리게이트 추가
    }

    void Update()
    {
        bool keyDown = Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0); // 키 입력 여부
        bool keyUp = Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.K) || Input.GetMouseButtonUp(0); // 키 입력 여부
        bool keyHold = Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K) || Input.GetMouseButton(0); // 키 입력 여부

        AttackEnemy(keyDown, keyUp);
        AttackLongEnemy(keyUp, keyHold);
    }

    private void AttackLongEnemy(bool keyUp, bool keyHold)
    {
        // 롱노트 공격 (누르면 따라가기 시작)
        if (keyHold && currentLongnote != null)
        {
            if (currentLongnote.CompareTag("LongnoteEnemy"))
            {
                currentLongnote.GetComponent<LongnoteEnemy>().StartFollowing();
            }
        }
        // 롱노트 공격 (때면 따라가기 종료)
        if (keyUp && currentLongnote != null)
        {
            if (currentLongnote.CompareTag("LongnoteEnemy"))
            {
                currentLongnote.GetComponent<LongnoteEnemy>().StopFollowing(); // 롱노트 적 공격 종료
                currentLongnote = null; // 롱노트 적 초기화
            }
        }
    }

    private void AttackEnemy(bool keyDown, bool keyUp)
    {
        // 캐릭터와 판정선의 위치 
        if (player != null)
        {
            transform.position = player.position + offset;
        }
        //공격
        if (keyDown && !isAttack)
        {
            attack();
        }
        if (keyUp)
        {
            isAttack = false; // 공격 상태 종료
        }
    }

    void attack()
    {
        currentJudge = JudgeResult.None; // 공격 시 판정 초기화
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
                    NoteHitPerfect?.Invoke(perfectScore); // 델리게이트 호출
                }
                else if (distance <= judgeWindow * 0.5f) // 50% 범위 내 : Nice
                {
                    currentJudge = JudgeResult.Nice;
                    NoteHitNice?.Invoke(niceScore); // 델리게이트 호출
                }
                // none이 아닐 경우 공격 시작
                if (currentJudge != JudgeResult.None)
                {
                    ApplyDamage(enemyObj.GetComponent<Collider2D>());
                    break; // 첫 번째 적을 찾으면 루프 종료
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
                enemy.TakeDamage(1);  // 데미지 1 적용
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LongnoteEnemy"))
        {
            currentLongnote = collision.gameObject; // 롱노트 적 등록
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LongnoteEnemy") && currentLongnote == collision.gameObject)
        {
            currentLongnote = null; // 판정선을 벗어나면 초기화
        }
    }

    void OnDestroy()
    {

        NoteHitPerfect -= scoreManager.AddPerfect;
        NoteHitNice -= scoreManager.AddNice;

        // 델리게이트 초기화
        NoteHitPerfect = null;
        NoteHitNice = null;
    }
}
