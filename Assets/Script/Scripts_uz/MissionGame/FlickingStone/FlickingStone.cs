using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickingStone : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpringJoint2D springJoint;
    private bool isPressed;
    private Camera Camera;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed){
            rb.position = UnityEngine.Camera.main.WorldToScreenPoint(Input.mousePosition);
        }
    }

    private void OnMouseDown(){
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp(){
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release(){
        yield return new WaitForSeconds(0.05f);

        GetComponent<SpringJoint2D>().enabled = false;
    }

    
}
