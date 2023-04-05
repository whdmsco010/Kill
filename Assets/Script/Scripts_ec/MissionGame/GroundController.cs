using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GroundController : MonoBehaviour
{
    Rigidbody2D rbody;
    public string targetObject;

    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == targetObject)
        {
            Debug.Log("Retry"); 
        }
    }
}