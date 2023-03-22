using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 position;
    public float Speed = 1;

    // Use this for initialization
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position.y += Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position.y -= Speed * Time.deltaTime;
        }
        transform.position = position;
    }
}
