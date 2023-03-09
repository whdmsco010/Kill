using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCamera : MonoBehaviour
{
    public GameObject B;
    Transform BT;

    void Start()
    {
        BT = B.transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(BT.position.x, BT.position.y, transform.position.z);
    }
}
