using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int move_method;

    Vector3 position;
    public float Speed = 1;

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
}