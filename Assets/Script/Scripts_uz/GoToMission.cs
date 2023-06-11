using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using Mono.Data.Sqlite;

//은채 스크립트 참고했습니다..ㅎㅎㅎ
public class GoToMission : MonoBehaviour
{
    public int sceneIndex;
    public GameObject Player;
    public GameObject targetObject;
    public string Pos_x;
    public string Pos_y;

    //-------------------- DB 연결 정의
    public string GM_DBFileName;
    public string GM_TableName;
    public DB_Controller GM_DBController;


    void Start(){

        DontDestroyOnLoad(Player);
        //파일명과 테이블명은 DemoSqlite에서 한 번에 처리가능하도록 참조함
        GM_DBFileName = GameObject.Find("Player").GetComponent<DemoSqlite>().DBFileName;
        GM_TableName = GameObject.Find("Player").GetComponent<DemoSqlite>().TableName;

        //----------DB_Controller.cs를 쓰기위한 경로 정의
        //위 스크립트는 Monobahaviour로 이뤄진게 아니라서 경로를 따로 지정해줘야 함
        string filePath = Path.Combine(Application.streamingAssetsPath, GM_DBFileName);
        GM_DBController = new DB_Controller("data source = " + filePath);
    }


    private void OnMouseDown()
    {

        if (targetObject == gameObject)
        {
            SceneManager.LoadScene(sceneIndex);
            Vector2 Pos = Player.transform.position;

            //SQL문을 사용하기위해 Vector를 String으로 전환해준다
            Pos_x=Pos.x.ToString();
            Pos_y=Pos.y.ToString();

            Debug.Log(Pos_x);
            Debug.Log(Pos.y);
            Debug.Log(GM_TableName);
            GM_DBController.UpdateInto(GM_TableName, new string[] { "location_x", "location_y"}, new string[] {Pos_x,Pos_y}, "player", "3");

        }

    }
}
