using Debug = UnityEngine.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class SQLiteConnector : MonoBehaviour
{
    private IDbConnection dbconn;

    // Start is called before the first frame update
    void Start()
    {
        // 데이터 연결
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/test.db"; //Path to database.

        Debug.Log(conn);
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.

        // 데이터 삽입
        //InsertData(3, 100, "New York", 10, "Use the red key");
        // 데이터 삭제
        //DeleteData(2);
        // 데이터 업데이트
        UpdateData(1, 90, "한글", 3, "ba");
        // 데이터 읽기
        ReadData();

        dbconn.Close();
        dbconn = null;
    }

    private void InsertData(int player, int score, string location, int kill, string hint)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();

        string insertSqlQuery = "INSERT INTO test (Player, Score, Location, Kill, Hint) VALUES (@player, @score, @location, @kill, @hint)";

        SqliteParameter playerParam = new SqliteParameter("@player", player);
        SqliteParameter scoreParam = new SqliteParameter("@score", score);
        SqliteParameter locationParam = new SqliteParameter("@location", location);
        SqliteParameter killParam = new SqliteParameter("@kill", kill);
        SqliteParameter hintParam = new SqliteParameter("@hint", hint);

        dbcmd.Parameters.Add(playerParam);
        dbcmd.Parameters.Add(scoreParam);
        dbcmd.Parameters.Add(locationParam);
        dbcmd.Parameters.Add(killParam);
        dbcmd.Parameters.Add(hintParam);

        dbcmd.CommandText = insertSqlQuery;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
    }

    private void DeleteData(int player)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();

        // 데이터 삭제
        string deleteSqlQuery = "DELETE FROM test WHERE Player = @playerToDelete";
        // 쿼리에 파라미터 추가
        SqliteParameter playerToDeleteParam = new SqliteParameter("@playerToDelete", player);
        // 파라미터를 IDbCommand의 Parameters 컬렉션에 추가
        dbcmd.Parameters.Add(playerToDeleteParam);

        dbcmd.CommandText = deleteSqlQuery;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
    }

    private void UpdateData(int player, int score, string location, int kill, string hint)
    {
        IDbCommand dbcmd = dbconn.CreateCommand();

        // 데이터 업데이트
        string updateSqlQuery = "UPDATE test SET (Score, Location, Kill, Hint) = (@scoreToUpdate, @locationToUpdate, @killToUpdate, @hintToUpdate) WHERE Player = @playerToUpdate";
        // 쿼리에 파라미터 추가
        SqliteParameter playerToUpdateParam = new SqliteParameter("@playerToUpdate", player);
        SqliteParameter scoreToUpdateParam = new SqliteParameter("@scoreToUpdate", score);
        SqliteParameter locationToUpdateParam = new SqliteParameter("@locationToUpdate", location);
        SqliteParameter killToUpdateParam = new SqliteParameter("@killToUpdate", kill);
        SqliteParameter hintToUpdateParam = new SqliteParameter("@hintToUpdate", hint);

        // 파라미터를 IDbCommand의 Parameters 컬렉션에 추가
        dbcmd.Parameters.Add(playerToUpdateParam);
        dbcmd.Parameters.Add(scoreToUpdateParam);
        dbcmd.Parameters.Add(locationToUpdateParam);
        dbcmd.Parameters.Add(killToUpdateParam);
        dbcmd.Parameters.Add(hintToUpdateParam);

        dbcmd.CommandText = updateSqlQuery;
        dbcmd.ExecuteNonQuery();

        dbcmd.Dispose();
        dbcmd = null;
    }

    private void ReadData()
    {
        IDbCommand dbcmd = dbconn.CreateCommand();

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
    }
}
