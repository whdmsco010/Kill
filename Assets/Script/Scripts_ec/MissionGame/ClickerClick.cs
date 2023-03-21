using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;

public class ClickerClick : MonoBehaviour
{
    int Click_N;
    public int Sword_N;
    public GameObject Backpack;
    public GameObject Clover;
    public TMP_Text Score;
    
    // Start is called before the first frame update
    void Start()
    {
        Sword_N = 1000;
        Score.text = "Click!!";
       
    }


    public void Click()
    {
        //Debug.Log("Click!!");
        Click_N++;
        Score.text = "    " + Click_N.ToString();
        //Debug.Log("Click : " + Click_N);
        if (Click_N == Sword_N)
        {
            Backpack.SetActive(false);
            Clover.SetActive(true);
        }
    }
}