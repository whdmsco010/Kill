using UnityEngine;
using System.IO;

public class DemoSqlite : MonoBehaviour
{
    public string m_DatabaseFileName = "test_uz3.db";
    public string m_TableName = "test_uz";
    private DatabaseAccess m_DatabaseAccess;

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);

        /*
        m_DatabaseAccess.CreateTable("test_uz",
            // 필드명, 타입명, not null, primary key, autoincrement, unique
            new string[,] { 
            {"player",      "INTEGER", "F", "T", "T", "F"},
            {"score",       "INTEGER", "T", "F", "F", "F"},
            {"location_x",  "INTEGER", "T", "F", "F", "F"},
            {"location_y",  "INTEGER", "T", "F", "F", "F"}, 
            {"kill",        "INTEGER", "T", "F", "F", "F"},
            {"hint",        "TEXT", "F", "F", "F", "F"} 
            }
        );
        */

        m_DatabaseAccess.InsertInto("test_uz", new string[] { "'1'", "'500'","'-10'","'40'","'0'","'None'" });
        m_DatabaseAccess.InsertInto("test_uz", new string[] { "'2'", "'300'","'5'","'-20'","'1'","'None'" });
        m_DatabaseAccess.InsertInto("test_uz", new string[] { "'3'", "'10'","'-10'","'2'","'0'","'테스트'" });
        

        m_DatabaseAccess.UpdateInto("test_uz", new string[] { "'score'"}, new string[] {"'1300'"}, "player", 2);
        //m_DatabaseAccess.UpdateInto("test_uz", new string[] { "'location_x','location_y'"}, new Vector2[]{playerPos.x, playerPos.y}, "player", 2);

        m_DatabaseAccess.CloseSqlConnection();

    }
}