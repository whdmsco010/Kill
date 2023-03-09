using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintGame : MonoBehaviour
{
    public GameObject BY;
    public GameObject BW;
    public GameObject BR;
    public GameObject RY;
    public GameObject RW;
    public GameObject WB;

    void Start()
    {
        int PaintInt = Random.Range(0, 6);
        if (PaintInt == 0)
        {
            BY.SetActive(true);
        }
        else if (PaintInt == 1)
        {
            BW.SetActive(true);
        }
        else if (PaintInt == 2)
        {
            BR.SetActive(true);
        }
        else if (PaintInt == 3)
        {
            RY.SetActive(true);
        }
        else if (PaintInt == 4)
        {
            RW.SetActive(true);
        }
        else if (PaintInt == 5)
        {
            WB.SetActive(true);
        }
    }
}
