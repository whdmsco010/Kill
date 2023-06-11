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
    public string DBFileName;
    public string TableName;
    public string HintTableName;
    public DB_Controller m_DatabaseAccess;

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, DBFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DB_Controller("data source = " + filePath);
        
        // ctrl + k + c 주석 삽입
        // crtl + k + u 주석 제거
        
        
        // m_DatabaseAccess.CreateTable(TableName,
        //     // 필드명, 타입명, not null, primary key, autoincrement, unique
        //     new string[,] { 
        //     {"player",      "INTEGER", "F", "T", "T", "F"},
        //     {"score",       "INTEGER", "T", "F", "F", "F"},
        //     {"location_x",  "REAL", "T", "F", "F", "F"},
        //     {"location_y",  "REAL", "T", "F", "F", "F"}, 
        //     {"kill",        "INTEGER", "T", "F", "F", "F"},
        //     {"hint",        "TEXT", "F", "F", "F", "F"} 
        //     }
        // );

        m_DatabaseAccess.CreateTable(HintTableName,
            //필드명, 타입명, not null, primary key, autoincrement, unique
            new string[,] { 
            {"player_index",      "INTEGER", "F", "F", "F", "F"},
            {"Hint",        "TEXT", "F", "F", "F", "F"},
            {"Item",        "TEXT", "F", "T", "F", "F"}
            }
        );


        // 모든 매개변수를 string으로 받아서  한 문장의 sql로 만든다음에 ExecuteQuery함수에 sql문장을 전해주고 
        // ExecutrQuery 함수에서 sql문을 해석함
        // 즉, int 필드의 값도 string 으로 전달해야 함 
        // ex) 1인경우, "1"
        // m_DatabaseAccess.InsertInto(TableName, new string[] { "'3'", "'500'","'-10'","'40'","'0'","'None'" });
        // m_DatabaseAccess.InsertInto(TableName, new string[] { "'4'", "'300'","'5'","'-20'","'1'","'None'" });
        // m_DatabaseAccess.UpdateInto(TableName, new string[] { "location_x", "location_y"}, new string[] {"'0'","'50'"}, "player", "3");
        m_DatabaseAccess.CloseSqlConnection();

    }
    
}