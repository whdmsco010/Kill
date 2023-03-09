using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance = null;

    private int sceneNum;
    private List<int> randomList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //randomList에 값 넣어주기
        ResetList();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (randomList.Count == 0 || randomList == null)
            {
                SceneManager.LoadScene(0);
                ResetList();
            }
            else
            {
                Mix();
                SceneManager.LoadScene(randomList[0]);
                randomList.RemoveAt(0);
            }
        }
    }

    //reset List
    private void ResetList()
    {
        //random하게 불러올 씬 넘버
        randomList = new List<int>() { 1, 2, 3, 4, 5, 6 };
    }

    //chk current sceneNum
    private int ChkScneNum()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        return sceneNum;
    }

    //shuffle
    public void Mix()
    {
        List<int> list = new List<int>();
        int count = randomList.Count;
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, randomList.Count);
            list.Add(randomList[rand]);
            randomList.RemoveAt(rand);
        }
        randomList = list;
    }
}