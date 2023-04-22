using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;
using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;

public class DemoSqlite : MonoBehaviour
{
    public string m_DatabaseFileName = "test_uz3.db";
    public string m_TableName = "test_uz3";
    private DB_Controller m_DatabaseAccess;

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DB_Controller("data source = " + filePath);
        
        // ctrl + k + c 주석 삽입
        // crtl + k + u 주석 제거
        
        /*
        m_DatabaseAccess.CreateTable("test_uz3",
            //필드명, 타입명, not null, primary key, autoincrement, unique
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
        m_DatabaseAccess.SqlQuery("INSERT into test_uz3(player, score, location_x, location_y, kill, hint) values (3,100,-10,10,0,\'테스트\')");
        m_DatabaseAccess.SqlQuery("INSERT into test_uz3(player, score, location_x, location_y, kill, hint) values (4,1300,1,6,1,\'테스트22222\')");
        m_DatabaseAccess.SqlQuery("Update test_uz3 set (location_x, location_y)=(555,777) where player = 2");
        m_DatabaseAccess.CloseSqlConnection();
        

        //delSqlQuery("DELETE FROM test_uz where player=3");

    }



    
}