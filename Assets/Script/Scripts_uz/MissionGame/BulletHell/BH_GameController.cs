using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BH_GameController : MonoBehaviour
{
    public GameObject bulletPrefab ;
    public GameObject uiStartGameObject ;
    public GameObject uiEndGameObject ;
    public Text uiTime ;
    int sec ;

    void Start() {    

    }

    public void PressStart() {

        sec = 0 ;

        uiStartGameObject.SetActive(false) ;
        InvokeRepeating("MakeBullet", 0f, 1f) ;
        InvokeRepeating("SetTime", 1f, 1f) ;        

    }

    public void PressRestart(){
        sec = 0;
        uiTime.text = ""+sec;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");

        foreach(GameObject bullet in bullets){
            Destroy (bullet);
        }

        uiEndGameObject.SetActive(false);
    }

    void SetTime() {

        sec = sec + 1 ;
        uiTime.text = "" + sec ;

    }

    void MakeBullet() {

        GameObject bullet ;

        float switchValue = Random.value ;
        float xValue = Random.Range(-13f, 13f) ;
        float yValue = Random.Range(-22f, 22f) ;

        if (switchValue > 0.5f) {

            if (Random.value > 0.5f) {

                bullet = Instantiate(bulletPrefab, new Vector3(-13f, yValue, 0f), Quaternion.identity) ;

            }
            else {

                bullet = Instantiate(bulletPrefab, new Vector3(13f, yValue, 0f), Quaternion.identity) ;

            }
            

        }
        else {
    
            if (Random.value > 0.5f) {

                bullet = Instantiate(bulletPrefab, new Vector3(xValue, -22f, 0f), Quaternion.identity) ;

            }
            else {

                bullet = Instantiate(bulletPrefab, new Vector3(xValue, 22f, 0f), Quaternion.identity) ;

            }

        }

    }
}
