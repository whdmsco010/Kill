using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningN : MonoBehaviour
{
    private int Timer = 0;
    public GameObject IMG_tutorial; // 튜토리얼 이미지

    // Start is called before the first frame update
    void Start()
    {
        //시작시 카운트 다운 초기화, 게임 시작 false 설정
        Timer = 0;
        // 튜토리얼, 나머지 (카운트다운 이미지) 안보이기
        IMG_tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //게임 시작시 정지
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }
        //Timer 가 90보다 작거나 같을경우 Timer 계속증가
        if (Timer <= 1000)
        {
            Timer++;


            // Timer가 60보다 작을경우 튜토리얼 켜기
            if (Timer < 1000)
            {
                IMG_tutorial.SetActive(true);
            }
            if (Timer > 1000)
            {
                IMG_tutorial.SetActive(false);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //게임시작
            }
        }
    }
    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        IMG_tutorial.SetActive(false);
    }
}