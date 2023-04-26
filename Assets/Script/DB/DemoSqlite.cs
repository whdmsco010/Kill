using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;
using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;

//using static DB_Controller;

public class DemoSqlite : MonoBehaviour
{
    public string DBFileName;
    public string TableName;
    private DB_Controller m_DatabaseAccess;

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, DBFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DB_Controller("data source = " + filePath);
        
        // ctrl + k + c 주석 삽입
        // crtl + k + u 주석 제거
        
        
        m_DatabaseAccess.CreateTable(TableName,
            //필드명, 타입명, not null, primary key, autoincrement, unique
            new string[,] { 
            {"Player",      "INTEGER", "F", "T", "T", "F"},
            {"Score",       "INTEGER", "T", "F", "F", "F"},
            {"Location_x",  "FLOAT", "T", "F", "F", "F"},
            {"Location_y",  "FLOAT", "T", "F", "F", "F"}, 
            {"Kill",        "INTEGER", "T", "F", "F", "F"},
            {"Hint",        "TEXT", "F", "F", "F", "F"} 
            }
        );
        
        m_DatabaseAccess.SqlQuery("INSERT into "+TableName+"(player, score, location_x, location_y, kill, hint) values (3, 100, 0.0, 0.0, 0, \'테스트\')");
        m_DatabaseAccess.CloseSqlConnection();
        

        //delSqlQuery("DELETE FROM test_uz where player=3");

    }



    
}