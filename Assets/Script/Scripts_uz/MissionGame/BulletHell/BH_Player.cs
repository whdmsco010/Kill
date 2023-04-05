using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BH_Player : MonoBehaviour
{
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed*Time.deltaTime,0,0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed*Time.deltaTime,0,0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0,speed*Time.deltaTime,0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0,-speed*Time.deltaTime,0));
        }
 
                   
    }


    void OnTriggerEnter2D(Collider2D collider){
        Destroy(collider.gameObject);
        GameObject.Find("game").GetComponent<BH_GameController>().uiEndGameObject.SetActive(true);
        Debug.Log("Game Over");
        
    }
}
