using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Select_T : MonoBehaviour
{

    public void check(InputField f) //InputField를 f로 선언
    { 
        if (f.text == "뉴진스의 하입보이요")
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Retry");
        }
    }
}