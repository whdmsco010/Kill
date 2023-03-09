using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrderScript : MonoBehaviour
{
    private int Timer = 0;
    public GameObject IMG_tutorial; // 튜토리얼 이미지
    public GameObject Num_A;   //1번
    public GameObject Num_B;   //2번
    public GameObject Num_C;   //3번
    public GameObject Num_D;   //시작 이미지
    public GameObject Num_E;
    public GameObject Num_F;
    public GameObject Num_G;
    public GameObject Num_H;
    public GameObject Num_I;
    

    void Start()
    {
        //시작시 카운트 다운 초기화, 게임 시작 false 설정
        Timer = 0;
        // 튜토리얼, 나머지 (카운트다운 이미지) 안보이기
        IMG_tutorial.SetActive(false);
        Num_A.SetActive(false);
        Num_B.SetActive(false);
        Num_C.SetActive(false);
        Num_D.SetActive(false);
        Num_E.SetActive(false);
        Num_F.SetActive(false);
        Num_G.SetActive(false);
        Num_H.SetActive(false);
        Num_I.SetActive(false);
        
    }

    void Update()
    {
        //게임 시작시 정지
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }
        //Timer 가 90보다 작거나 같을경우 Timer 계속증가
        if (Timer <= 1900)
        {
            Timer++;


            // Timer가 60보다 작을경우 튜토리얼 켜기
            if (Timer < 100)
            {
                IMG_tutorial.SetActive(true);
            }
            // Timer가 60보다 클경우 튜토리얼 끄고 3켜기
            if (Timer > 100)
            {
                IMG_tutorial.SetActive(false);
                Num_A.SetActive(true);
            }
            // Timer가 90보다 작을경우 3끄고 2켜기
            if (Timer > 300)
            {
                Num_A.SetActive(false);
                Num_B.SetActive(true);
            }
            // Timer 가 120보다 클경우 2끄고 1켜기
            if (Timer > 500)
            {
                Num_B.SetActive(false);
                Num_C.SetActive(true);
            }
            if (Timer > 700)
            {
                Num_C.SetActive(false);
                Num_D.SetActive(true);
            }
            if (Timer > 900)
            {
                Num_D.SetActive(false);
                Num_E.SetActive(true);
            }
            if (Timer > 1100)
            {
                Num_E.SetActive(false);
                Num_F.SetActive(true);
            }
            if (Timer > 1300)
            {
                Num_F.SetActive(false);
                Num_G.SetActive(true);
            }
            if (Timer > 1500)
            {
                Num_G.SetActive(false);
                Num_H.SetActive(true);
            }
            
            // Timer 가 150보다 크거나 같을 경우 1끄고 시작 켜기 LoadingEnd () 코루틴호출
            if (Timer >= 1600)
            {
                Num_H.SetActive(false);
                Num_I.SetActive(true);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //게임시작
            }
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        Num_I.SetActive(true);
    }

}