using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//은채 스크립트 참고했습니다..ㅎㅎㅎ
public class GoToMission : MonoBehaviour
{
    public int sceneIndex;
    public GameObject targetObject;
    private void OnMouseDown()
    {

        if (targetObject == gameObject)
        {
            //SceneManager.LoadScene(sceneIndex);
            GameObject.Find("Player").GetComponent<MovingObject>().PlayerPosition_DB();
        }

    }
}
