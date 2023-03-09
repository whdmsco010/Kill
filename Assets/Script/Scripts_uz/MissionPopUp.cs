using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPopUp : MonoBehaviour
{

    public GameObject missionPanel;
    // Start is called before the first frame update
    
    void OnMouseDown()
    {
        missionPanel.SetActive(true);
    }

    /*void Update()
    {
       //If (Input.GetMouseButtonUp(0))
        ///Debug.Log("클릭");
       
    }*/
    
}
