using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;


public class SQLiteConnector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 데이터 연결
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/test.db"; //Path to database.

        Debug.Log(conn);
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        /*
        // 데이터 삽입
        string insertSqlQuery = "INSERT INTO test (Player, Score, Location, Kill, Hint) VALUES (@player, @score, @location, @kill, @hint)";
        // 쿼리에 파라미터 추가
        SqliteParameter playerParam = new SqliteParameter("@player", 3);
        SqliteParameter scoreParam = new SqliteParameter("@score", 100);
        SqliteParameter locationParam = new SqliteParameter("@location", "New York");
        SqliteParameter killParam = new SqliteParameter("@kill", 10);
        SqliteParameter hintParam = new SqliteParameter("@hint", "Use the red key");
        // 파라미터를 IDbCommand의 Parameters 컬렉션에 추가
        dbcmd.Parameters.Add(playerParam);
        dbcmd.Parameters.Add(scoreParam);
        dbcmd.Parameters.Add(locationParam);
        dbcmd.Parameters.Add(killParam);
        dbcmd.Parameters.Add(hintParam);

        dbcmd.CommandText = insertSqlQuery;
        dbcmd.ExecuteNonQuery();*/


        // 데이터 읽기
        string sqlQuery = "SELECT Player, Score, Location, Kill, Hint " + "FROM test";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int player = reader.GetInt32(0);
            int score = reader.GetInt32(1);
            string location = reader.GetString(2);
            int kill = reader.GetInt32(3);
            string hint = reader.GetString(4);

            Debug.Log("Player " + player + "  Score =" + score + "  Location =" + location + "  Kill =" + kill + "  Hint =" + hint);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

}