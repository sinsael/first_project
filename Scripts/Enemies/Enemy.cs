using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;  // 체력
    private Vector3 initialPosition;  // 리스폰 위치
    private int respawnHealth;  // 리스폰 체력
    public float knockbackDistance = 1f;  // 넉백 거리
    private bool isKnockedBack = false;  // 넉백 여부
    public float knockbackDuration = 0.25f;  // 넉백 지속 시간
    public event Action<int, Vector3> OnHealthDecreased;  // 체력 감소 이벤트

    void Start()
    {
        initialPosition = transform.position;
        respawnHealth = health;
    }

    public void TakeDamage(int damage, Vector3 attackerPosition)
    {
        health -= damage;

        if (!isKnockedBack)
        {
            // 오른쪽 방향으로만 넉백되도록 설정
            Vector3 knockbackDirection = Vector3.right;  // 오른쪽 방향으로 고정
            Vector3 targetPosition = transform.position + (knockbackDirection * knockbackDistance);  // 넉백 목표 위치 계산

            StartCoroutine(Knockback(targetPosition));
        }

        if (health <= 0)
        {
            Die();
            Invoke("Respawn", 2f);
        }

        // 체력 감소 이벤트를 적의 현재 위치와 함께 전달
        OnHealthDecreased?.Invoke(damage, transform.position);
    }

    IEnumerator Knockback(Vector3 targetPosition)
    {
        isKnockedBack = true;  // 넉백 시작
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

    void Respawn()
    {
        transform.position = initialPosition;  // 원래 위치로 이동
        health = respawnHealth;  // 체력 초기화
        gameObject.SetActive(true);  // 활성화
        isKnockedBack = false;  // 넉백 초기화
    }
}
