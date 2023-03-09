using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    public GameObject target;

    void OnMouseDown()
    {
        target.SetActive(true);  //open
    }
}
