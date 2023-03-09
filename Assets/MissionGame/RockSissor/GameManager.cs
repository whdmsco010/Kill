using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject success;
    public GameObject vs;

    //objects
    public Image MyImage;
    public Image ComImage;

    public Text txtMy;
    public Text txtCom;
    public Text txtScore;

    public Text rock;
    public Text scissors;
    public Text paper;

    //int you = 0;        //rock:1, paper:2, scissors:3
    int com = 0;
    int scoreMy = 0;    //vs
    int scoreCom = 0;

    void CheckScore(int myHand)
    {
        com = Random.Range(1, 4);
        ComImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Game_" + com) as Sprite;
        MyImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Game_" + myHand) as Sprite;

        int k = myHand - com;

        if(k==0)
        {
            txtScore.text = "비겼습니다";
        }
        else if(k == 1 || k== -2)
        {
            txtScore.text = "이겼습니다";
            scoreMy++;
        }
        else
        {
            txtScore.text = "졌습니다";
            scoreCom++;
        }

        txtMy.text = "ME : " + scoreMy;
        txtCom.text = "COM : " + scoreCom;

        myHand = 0;

        if(scoreMy >= 3)
        {
            vs.SetActive(false);
            success.SetActive(true);
        }

    }

    public void ClickScissors()
    {
        scissors.text = "scissors";

        CheckScore(1);
    }

    public void ClickRock()
    {
        rock.text = "rock";

        CheckScore(2);
    }

    public void ClickPaper()
    {
        paper.text = "paper";

        CheckScore(3);
    }
}
