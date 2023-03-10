using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ClickerNotice : MonoBehaviour
{

    void Start()
    {

    }
    public void onClick()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        else if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }
}