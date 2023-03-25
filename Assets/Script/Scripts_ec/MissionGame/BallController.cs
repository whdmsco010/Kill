using System;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float force = 5f; // 공에 가할 힘의 크기
    public float maxAngle = 45f; // 공이 튕길 최대 각도
    public TMP_Text Score; // 점수를 표시할 UI 텍스트
    private Rigidbody2D rb; // 공의 Rigidbody 2D 컴포넌트
    private int score = 0; // 현재 점수
    public string targetObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // 중력 값 설정
        UpdateScoreText(); // 초기 점수 값 설정
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == targetObject) // 땅에 충돌한 경우
        {
            Destroy(gameObject); // 공 제거
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnMouseDown()
    {
        rb.velocity = Vector2.zero; // 이전 힘 초기화

        float angle = Random.Range(-maxAngle, maxAngle); // 랜덤한 각도 생성
        Vector2 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.up; // 각도를 방향 벡터로 변환

        rb.AddForce(direction * force, ForceMode2D.Impulse); // 힘을 가함

        score++; // 점수 증가
        UpdateScoreText(); // 텍스트 업데이트
        if (score == 20)
        {
            Time.timeScale = 0;
            Debug.Log("Success!!");
        }
    }

    void UpdateScoreText()
    {
        Score.text = "Score: " + score; // 텍스트 업데이트
    }
}