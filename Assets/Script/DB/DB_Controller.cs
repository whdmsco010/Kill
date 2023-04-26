using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using static DemoSqlite;

public class DB_Controller
{
    private SqliteConnection m_DatabaseConnection;
    private SqliteCommand m_DatabaseCommand;
    private SqliteDataReader m_Reader;

    // DemoSqlite 말고 따른 cs에서 이름을 선언해줄 cs가 필요함. 밑에가 있을때 없을때가 있는데 사용하기 위해선
    // 계속 db컨트롤러 불러와야하는데 얘가 방해함
    public string m_DatabaseFileName = GameObject.Find("Main Camera").GetComponent<DemoSqlite>().DBFileName;
    public string m_TableName = GameObject.Find("Main Camera").GetComponent<DemoSqlite>().TableName;

    public DB_Controller(string connectionString)
    {
        OpenDatabase(connectionString);
    }

    public void OpenDatabase(string connectionString)
    {
        m_DatabaseConnection = new SqliteConnection(connectionString);
        m_DatabaseConnection.Open();
        Debug.Log("Connected to database");
    }

    public void CloseSqlConnection()
    {
        if (m_DatabaseCommand != null)
        {
            m_DatabaseCommand.Dispose();
        }

        m_DatabaseCommand = null;

        if (m_Reader != null)
        {
            m_Reader.Dispose();
        }

        m_Reader = null;

        if (m_DatabaseConnection != null)
        {
            m_DatabaseConnection.Close();
        }

        m_DatabaseConnection = null;
        Debug.Log("Disconnected from database.");
    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        m_DatabaseCommand = m_DatabaseConnection.CreateCommand();
        m_DatabaseCommand.CommandText = sqlQuery;

        m_Reader = m_DatabaseCommand.ExecuteReader();

        return m_Reader;
    }

    
    public SqliteDataReader CreateTable(string tablename, string[,] field)
    {
        // 테이블명, 필드명, 타입명, not null, primary key, autoincrement, unique
        if (field.GetLength(1) != 6) // 열의 개수가 6개가 아니면 필요한 정보가 부족한 것
        {
            throw new SqliteException("테이블 작성시 필요한 정보가 부족합니다");
        }

        string query = "CREATE TABLE " + tablename + " (" ;
        string PKquery = null;
        string PK_field = null;
        string AI_field = null;
        int flag_PK = 0;

        //1행은 for문 밖에서 수행
        query += field[0,0] + " " + field[0,1] + " ";
        if(field[0,2]=="T")   query += "NOT NULL" + " ";
        if(field[0,5]=="T")   query += "UNIQUE";
        if(field[0,3]=="T")   PKquery += field[0,0];

        //2행부터 출력하기 위해 카운트를 1부터 함
        for (int i= 1; i<field.GetLength(0); ++i){ //행
            query += ", " + field[i,0] + " " + field[i,1] + " ";
            if(field[i,2]=="T")   query += "NOT NULL" + " ";
            if(field[i,5]=="T")   query += "UNIQUE";   
            if(field[i,3]=="T")   PKquery += ", " + field[i,0];
        }

        
        // 기본키 추가하는 구문. 기본키가 있다면 플래그 값을 0->1로 바꾸고 sql 구문 삽입
        int cnt_PK = 0;
        for(int m=0; m<field.GetLength(0); ++m){
            if(field[m,3]=="T") {
                flag_PK = 1; 
                cnt_PK++;
                PK_field = field[m,0];
            }
        }
        if(flag_PK == 1) query += ", PRIMARY KEY(" + PKquery;


        // 자동인덱스를 위한 구문. 자동인덱스 설정시, 기본키가 단독일때만 설정가능, 자동인덱스는 테이블에서 하나의 필드에만 가능
        int cnt_AI = 0;
        for(int n=0; n<field.GetLength(0); ++n){
            if(field[n,4]=="T"){
                cnt_AI++;
                AI_field = field[n,0];
            }
        }
        if(PK_field == AI_field && cnt_AI == 1 && cnt_PK == 1){
            query += " AUTOINCREMENT)";
        }
        else{
            throw new SqliteException(" AUTOINCREMENT를 설정하기위한 조건을 만족하지 않습니다");
        }


        query += ")";
        return ExecuteQuery(query);
    }

    public string FilePath(){
        string str = string.Empty;
        str = "URI=file:" + Application.dataPath + "/StreamingAssets/" + m_DatabaseFileName;

        return str;
    }

    public void SqlQuery(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(FilePath());
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