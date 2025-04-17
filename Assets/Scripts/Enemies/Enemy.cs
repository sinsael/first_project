using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< HEAD
=======
    [Header("적 스탯")]
    [SerializeField]private int health;  // 체력
    [Header("넉백 설정")]
    [Tooltip("넉백 거리")]
    [SerializeField]float knockbackDistance = 1f;  // 넉백 거리
    private bool isKnockedBack = false;  // 넉백 여부
    [Tooltip("넉백 지속 시간")]
    [SerializeField] float knockbackDuration = 0.25f;  // 넉백 지속 시간
    [Tooltip("적 사망 시 호출되는 이벤트")]
    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
        respawnHealth = health;
>>>>>>> 8d537552cc4702e898e5af7ec8a37b827cd5dfad

    [SerializeField] private int health;  // 체력
    [SerializeField] float knockbackDistance = 1f;  // 넉백 거리

    [SerializeField] float knockbackDuration = 0.25f;  // 넉백 지속 시간
    private bool isKnockedBack = false;  // 넉백 여부

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (!isKnockedBack)
        {
            Vector3 targetPosition = Knockbackbool();

            StartCoroutine(Knockback(targetPosition));
        }

        if (health <= 0)
        {
            Die();
        }

    }

    private Vector3 Knockbackbool()
    {
        // 오른쪽 방향으로만 넉백되도록 설정
        Vector3 knockbackDirection = Vector3.right;  // 오른쪽 방향으로 고정
        Vector3 targetPosition = transform.position + (knockbackDirection * knockbackDistance);  // 넉백 목표 위치 계산
        return targetPosition;
    }

    IEnumerator Knockback(Vector3 targetPosition)
    {
        isKnockedBack = true;  // 넉백 시작
        //넉백 애니메이션 넣기
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / knockbackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;  // 최종 위치 보정
        isKnockedBack = false;  // 넉백 끝
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
