using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void SceneChange()
    {
       int Map = Random.Range(1, 5);
       SceneManager.LoadScene(Map);
    }
}
