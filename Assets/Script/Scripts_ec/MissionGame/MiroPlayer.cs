using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class MiroPlayer : MonoBehaviour
{
    public int move_method;

    Vector3 position;
    public float Speed = 1;
    Rigidbody2D rbody;
    public string targetObject;
    public string targetObject2;

    void Start()
    {
        Time.timeScale = 2;
    }
    void Update()
    {
        if (move_method == 0)
        {
            position = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += Speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x -= Speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += Speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= Speed;
            }

            transform.position = position;
        }

        else
        {
            position = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x += Speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x -= Speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += Speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= Speed;
            }

            GetComponent<Rigidbody2D>().velocity = position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌오브젝트가 타겟 오브젝트라면.. 게임을 중지
        if (collision.gameObject.name == targetObject)
        {
            Time.timeScale = 0;
            Debug.Log("Retry");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.gameObject.name == targetObject2)
        {
            Debug.Log("Sucess!!");
        }
    }
}