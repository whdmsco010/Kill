using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Select_T : MonoBehaviour
{

    public void check(InputField f) //InputField�� f�� ����
    { 
        if (f.text == "�������� ���Ժ��̿�")
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Retry");
        }
    }
}