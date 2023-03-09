using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coupon : MonoBehaviour
{
    int count = 0;
    public GameObject success;

    void OnMouseDown()
    {
        count = count + 1;
        if (count >= 10)
        {
            success.SetActive(true);
            count = 0;
        }
    }
}