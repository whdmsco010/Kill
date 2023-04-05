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
    public float force = 5f; // ���� ���� ���� ũ��
    public float maxAngle = 45f; // ���� ƨ�� �ִ� ����
    public TMP_Text Score; // ������ ǥ���� UI �ؽ�Ʈ
    private Rigidbody2D rb; // ���� Rigidbody 2D ������Ʈ
    private int score = 0; // ���� ����
    public string targetObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // �߷� �� ����
        UpdateScoreText(); // �ʱ� ���� �� ����
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == targetObject) // ���� �浹�� ���
        {
            Destroy(gameObject); // �� ����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnMouseDown()
    {
        rb.velocity = Vector2.zero; // ���� �� �ʱ�ȭ

        float angle = Random.Range(-maxAngle, maxAngle); // ������ ���� ����
        Vector2 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.up; // ������ ���� ���ͷ� ��ȯ

        rb.AddForce(direction * force, ForceMode2D.Impulse); // ���� ����

        score++; // ���� ����
        UpdateScoreText(); // �ؽ�Ʈ ������Ʈ
        if (score == 20)
        {
            Time.timeScale = 0;
            Debug.Log("Success!!");
        }
    }

    void UpdateScoreText()
    {
        Score.text = "Score: " + score; // �ؽ�Ʈ ������Ʈ
    }
}