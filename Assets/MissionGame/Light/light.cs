using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public GameObject Tog1;
    public GameObject Tog2;
    public GameObject Tog3;

    public GameObject success;

    int count1 = 0;
    int count2 = 0;
    int count3 = 0;

    void LightSuccess(int one, int two, int three)
    {
        if ((one % 2 == 1) && (two % 2 == 1) && (three % 2 == 1))
        {
            success.SetActive(true);
        }
        else success.SetActive(false);
    }

    public void LightTog1()
    {
        count1 = count1 + 1;
        LightSuccess(count1, count2, count3);
    }

    public void LightTog2()
    {
        count2 = count2 + 1;
        LightSuccess(count1, count2, count3);
    }

    public void LightTog3()
    {
        count3 = count3 + 1;
        LightSuccess(count1, count2, count3);
    }
}
