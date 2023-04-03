using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGame : MonoBehaviour
{
    public GameObject success;
    private Coroutine coroutine;

    public RectTransform Player;
    public Image Dice;

    int player_count = 0;
    int dice_count = 0;

    void Start()
    {
        Player.anchoredPosition = new Vector2(-327.9f, -141.1f);
    }

    public void ClickDice()
    {
        dice_count = Random.Range(1, 7);
        player_count = player_count + dice_count;
        Dice.GetComponent<Image>().sprite = Resources.Load<Sprite>("Board_" + dice_count) as Sprite;

        MovePlayer(player_count);
    }

    public void OnInvoke()
    {
        Debug.Log("2초 기다림");
    }

    private IEnumerator WaitAndPrint(float waitTime, int player_count)
    {
        yield return new WaitForSeconds(waitTime); //waitTime 만큼 딜레이후 다음 코드가 실행된다.
        MovePlayer(player_count);
    }

    public void MovePlayer(int count)
    {
        if (count == 0)
        {
            Player.anchoredPosition = new Vector2(-327.9f, -141.1f);
        }
        else if (count == 1)
        {
            Player.anchoredPosition = new Vector2(-267.9f, -141.1f);
        }
        else if (count == 2)
        {
            Player.anchoredPosition = new Vector2(-207.9f, -141.1f);
        }
        else if (count == 3)
        {
            Player.anchoredPosition = new Vector2(-147.9f, -141.1f);
            player_count = 4;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 4)
        {
            Player.anchoredPosition = new Vector2(-87.90002f, -141.1f);
        }
        else if (count == 5)
        {
            Player.anchoredPosition = new Vector2(-27.9f, -141.1f);
            player_count = 7;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 6)
        {
            Player.anchoredPosition = new Vector2(32.09998f, -141.1f);
            player_count = 4;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 7)
        {
            Player.anchoredPosition = new Vector2(92.09998f, -141.1f);
        }
        else if (count == 8)
        {
            Player.anchoredPosition = new Vector2(152.1f, -141.1f);
            player_count = 16;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 9)
        {
            Player.anchoredPosition = new Vector2(212.0999f, -141.1f);
            player_count = 7;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 10)
        {
            Player.anchoredPosition = new Vector2(272.0999f, -141.1f);
        }
        else if (count == 11)
        {
            Player.anchoredPosition = new Vector2(332.0999f, -141.1f);
            player_count = 14;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 12)
        {
            Player.anchoredPosition = new Vector2(332.1f, -81.1f);
            player_count = 14;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 13)
        {
            Player.anchoredPosition = new Vector2(332.1f, -21.1f);
            player_count = 10;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 14)
        {
            Player.anchoredPosition = new Vector2(272.1f, -21.1f);
        }
        else if (count == 15)
        {
            Player.anchoredPosition = new Vector2(212.1f, -21.1f);
            player_count = 10;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 16)
        {
            Player.anchoredPosition = new Vector2(152.1f, -21.1f);
        }
        else if (count == 17)
        {
            Player.anchoredPosition = new Vector2(92.1f, -21.1f);
            player_count = 0;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 18)
        {
            Player.anchoredPosition = new Vector2(32.1f, -21.1f);
            player_count = player_count + dice_count;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 19)
        {
            Player.anchoredPosition = new Vector2(-27.9f, -21.1f);
        }
        else if (count == 20)
        {
            Player.anchoredPosition = new Vector2(-87.89999f, -21.1f);
            player_count = 30;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 21)
        {
            Player.anchoredPosition = new Vector2(-147.9f, -21.1f);
            player_count = 26;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 22)
        {
            Player.anchoredPosition = new Vector2(-207.9f, -21.1f);
            player_count = 19;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 23)
        {
            Player.anchoredPosition = new Vector2(-267.9f, -21.1f);
            player_count = 26;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 24)
        {
            Player.anchoredPosition = new Vector2(-327.9f, -21.1f);
            player_count = 19;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 25)
        {
            Player.anchoredPosition = new Vector2(-327.8999f, 38.90001f);
        }
        else if (count == 26)
        {
            Player.anchoredPosition = new Vector2(-327.9f, 98.90002f);
        }
        else if (count == 27)
        {
            Player.anchoredPosition = new Vector2(-267.9f, 98.90002f);
            player_count = 25;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 28)
        {
            Player.anchoredPosition = new Vector2(-207.9f, 98.90002f);
            player_count = 29;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 29)
        {
            Player.anchoredPosition = new Vector2(-147.9f, 98.90002f);
        }
        else if (count == 30)
        {
            Player.anchoredPosition = new Vector2(-87.89996f, 98.90002f);
        }
        else if (count == 31)
        {
            Player.anchoredPosition = new Vector2(-27.9f, 98.90002f);
            player_count = player_count + dice_count;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 32)
        {
            Player.anchoredPosition = new Vector2(32.09998f, 98.90002f);
            player_count = 0;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 33)
        {
            Player.anchoredPosition = new Vector2(92.09998f, 98.90002f);
            player_count = 29;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 34)
        {
            Player.anchoredPosition = new Vector2(152.1f, 98.90002f);
        }
        else if (count == 35)
        {
            Player.anchoredPosition = new Vector2(212.0999f, 98.90002f);
            player_count = 30;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count == 36)
        {
            Player.anchoredPosition = new Vector2(272.0999f, 98.90002f);
            player_count = 14;

            coroutine = StartCoroutine(WaitAndPrint(1.5f, player_count));
        }
        else if (count >= 37)
        {
            Player.anchoredPosition = new Vector2(332.0999f, 98.90002f);
            success.SetActive(true);
        }
    }
}
