using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningN : MonoBehaviour
{
    private int Timer = 0;
    public GameObject IMG_tutorial; // Ʃ�丮�� �̹���

    // Start is called before the first frame update
    void Start()
    {
        //���۽� ī��Ʈ �ٿ� �ʱ�ȭ, ���� ���� false ����
        Timer = 0;
        // Ʃ�丮��, ������ (ī��Ʈ�ٿ� �̹���) �Ⱥ��̱�
        IMG_tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���۽� ����
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }
        //Timer �� 90���� �۰ų� ������� Timer �������
        if (Timer <= 1000)
        {
            Timer++;


            // Timer�� 60���� ������� Ʃ�丮�� �ѱ�
            if (Timer < 1000)
            {
                IMG_tutorial.SetActive(true);
            }
            if (Timer > 1000)
            {
                IMG_tutorial.SetActive(false);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //���ӽ���
            }
        }
    }
    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        IMG_tutorial.SetActive(false);
    }
}