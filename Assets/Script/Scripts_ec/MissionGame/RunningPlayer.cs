using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using Debug = UnityEngine.Debug;

public class RunningPlayer : MonoBehaviour
{
    public int move_method;

    Vector3 position;
    public float Speed = 1;
    Rigidbody2D rbody;
    public string targetObject;
    public string targetObject1;
    public string targetObject2;
    public string targetObject3;
    public string targetObject4;
    public string targetObject5;
    public string targetObject6;


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
                position.x -= Speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x += Speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y -= Speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y += Speed;
            }

            transform.position = position;
        }

        else
        {
            position = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                position.x -= Speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                position.x += Speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y -= Speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y += Speed;
            }

            GetComponent<Rigidbody2D>().velocity = position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌오브젝트가 타겟 오브젝트라면.. 게임을 중지
        if (collision.gameObject.name == targetObject || collision.gameObject.name == targetObject1 || collision.gameObject.name == targetObject2 || collision.gameObject.name == targetObject3 || collision.gameObject.name == targetObject4 || collision.gameObject.name == targetObject5)
        {
            Time.timeScale = 0;
            Debug.Log("Retry");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.gameObject.name == targetObject6)
        {
            Time.timeScale = 0;
            Debug.Log("Success!!");
            SceneManager.LoadScene("FarmScene");
        }
    }
}