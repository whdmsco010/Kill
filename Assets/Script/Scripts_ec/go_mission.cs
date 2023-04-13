using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class go_mission : MonoBehaviour
{
    public int sceneIndex;
    public GameObject targetObject;

    private void OnMouseDown()
    {
        if (targetObject == gameObject)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}