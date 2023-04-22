using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class SQLite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //데이터 연결
    public string GetDBFilePath()
    {
        string str = string.Empty;
        str = "URI=file:" + Application.dataPath + "/StreamingAssets/test.db";

        return str;
    }

    //SQL 삭제, 삽입, 수정 다 되는 코드
    public void delSqlQuery(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
}
