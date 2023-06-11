using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DB_Controller
{
    private SqliteConnection m_DatabaseConnection;
    private SqliteCommand m_DatabaseCommand;
    private SqliteDataReader m_Reader;

    public string m_DatabaseFileName = GameObject.Find("Player").GetComponent<DemoSqlite>().DBFileName;
    public string m_TableName = GameObject.Find("Player").GetComponent<DemoSqlite>().TableName;

    public DB_Controller(string connectionString)
    {
        OpenDatabase(connectionString);
    }

    public void OpenDatabase(string connectionString)
    {
        // DB가 연결되면 Connected to DB 로그 표시
        m_DatabaseConnection = new SqliteConnection(connectionString);
        m_DatabaseConnection.Open();
        Debug.Log("Connected to database");
    }

    public void CloseSqlConnection()
    {
        // DB연결 종료
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
        // 밑의 함수들에서 string으로 조합된 sqlQuery를 전달받으면 
        // SQL문으로 해석해서 쿼리를 실행함

        m_DatabaseCommand = m_DatabaseConnection.CreateCommand();
        m_DatabaseCommand.CommandText = sqlQuery;

        m_Reader = m_DatabaseCommand.ExecuteReader();

        return m_Reader;
    }

    
    public SqliteDataReader CreateTable(string tablename, string[,] Fields)
    {
        // 테이블명, 필드명(i,0), 타입명(i,1), not null(i,2), primary key(i,3), autoincrement(i,4), unique(i,5)
        if (Fields.GetLength(1) != 6) // 열의 개수가 6개가 아니면 필요한 정보가 부족한 것
        {
            throw new SqliteException("테이블 작성시 필요한 정보가 부족합니다");
        }

        string query = "CREATE TABLE " + tablename + " (" ;
        string PKquery = null;                              // PRIMARY KEY (@) : @부분에 들어가는 쿼리
        string PK_field = null;                             // 기본키 필드명을 저장하는 변수
        string AI_field = null;                             // AI 필드명을 저장하는 변수
        int flag_PK = 0;                                    // 테이블에 기본키가 있는지 확인하기 위한 플래그(체크표시 같은것)


        // 1행은 for문 밖에서 수행
        // 필드명(i,0), 타입명(i,1), not null(i,2), primary key(i,3), autoincrement(i,4), unique(i,5)
        query += Fields[0,0] + " " + Fields[0,1] + " ";
        if(Fields[0,2]=="T")   query += "NOT NULL" + " ";
        if(Fields[0,5]=="T")   query += "UNIQUE";
        if(Fields[0,3]=="T")   PKquery += Fields[0,0];


        // 2행부터 출력하기 위해 카운트를 1부터 함
        // 필드명(i,0), 타입명(i,1), not null(i,2), primary key(i,3), autoincrement(i,4), unique(i,5)
        for (int i= 1; i<Fields.GetLength(0); ++i){ //행
            query += ", " + Fields[i,0] + " " + Fields[i,1] + " ";
            if(Fields[i,2]=="T")   query += "NOT NULL" + " ";
            if(Fields[i,5]=="T")   query += "UNIQUE";   
            if(Fields[i,3]=="T")   PKquery += ", " + Fields[i,0];
        }

        
        // 기본키 추가하는 구문. 기본키가 있다면 플래그 값을 0->1로 바꾸고 sql 구문 삽입
        int cnt_PK = 0;
        for(int m=0; m<Fields.GetLength(0); ++m){
            if(Fields[m,3]=="T") {
                flag_PK = 1;                                    // 기본키 필드가 True이기때문에 플래그를 0->1로 바꿈
                cnt_PK++;                                       // 기본키의 개수를 저장함 (기본키는 여러개를 묶어서 가능하기 때문)
                PK_field = Fields[m,0];                         //기본키 필드명을 변수에 저장함
            }
        }
        if(flag_PK == 1) query += ", PRIMARY KEY(" + PKquery;


        // 자동인덱스를 위한 구문. 자동인덱스 설정시, 기본키가 단독일때만 설정가능, 자동인덱스는 테이블에서 하나의 필드에만 가능
        int cnt_AI = 0;
        for(int n=0; n<Fields.GetLength(0); ++n){
            if(Fields[n,4]=="T"){
                cnt_AI++;                                       // AI의 개수를 저장함 (AI는 1개만 되기 때문에 추후의 유효성 검사에 사용하기 위함)
                AI_field = Fields[n,0];                         // AI 필드명을 변수에 저장함
            }
        }

        for(int n=0; n<Fields.GetLength(0); ++n){
            if(Fields[n,4]=="T"){
                if(PK_field == AI_field && cnt_AI == 1 && cnt_PK == 1){ // 기본키=AI 필드가 같고, AI가 1개이며, 기본키가 1개일때만 AUTOINCREMENT가 가능함
                    query += " AUTOINCREMENT)";
                }
                else{
                    throw new SqliteException(" AUTOINCREMENT를 설정하기위한 조건을 만족하지 않습니다");
                }
            }
        }


        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader ReadFullTable(string tableName)
    {
        // 전체 테이블 레코드를 읽어옴
        // ex) SELECT * FROM test_uz5
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        // 아예 새로운 레코드를 생성하고싶은 경우
        // 테이블에 지정한 필드 순서대로 값을 지정해줘야 함
        // ex) INSERT INTO test_uz5 VALUES (1, 100, 0, 0, 테스트)

        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertIntoSpecific(string tableName, string[] Fields, string[] values)
    {
        // 특정 레코드에 특정 필드들만 생성하고싶은 경우
        // ex) INSERT INTO test_uz (location_x,score) VALUES (0, 1500)
        if  (Fields.Length != values.Length)                    // 필드 2개를 추가하는 경우 값도 2개가 필요함
        {   
            throw new SqliteException("Fields.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + Fields[0];
        for (int i = 1; i < Fields.Length; ++i)
        {
            query += ", " + Fields[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }


    // 모든 매개변수를 string으로 받아서  한 문장의 sql로 만든다음에 ExecuteQuery함수에 sql문장을 전해주고 
    // ExecutrQuery 함수에서 sql문을 해석함
    // 즉, int 필드의 값도 string 으로 전달해야 함 
    // ex) 1인경우, "1"

    public SqliteDataReader UpdateInto(string tableName, string[] Fields, string[] Fieldsvalues, string selectkey, string selectvalue)
    {
        // 레코드 업데이트도 insert specific 처럼 여러 필드를 동시에 업데이트 가능함
        // 다만 필드 순서와 값 순서를 일치시켜야 함
        // ex) UPDATE test_uz SET {location_x,locationy} = {100,-100}
        string query = "UPDATE " + tableName + " SET " + Fields[0] + " = " + Fieldsvalues[0];

        for (int i = 1; i < Fieldsvalues.Length; ++i)
        {

            query += ", " + Fields[i] + " =" + Fieldsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }


    public SqliteDataReader DeleteContents(string tableName)
    {
        // ex) DELETE FROM test_uz
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }



    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] Field, string[] operation, string[] values)
    {
        // 조건문으로 특정 레코드 찾기
        // ex) SELECT 플레이어 FROM 게임정보 WHERE 점수 LIKE 값
        // ex) SELECT player FROM test_uz WHERE score like _5%
        if (Field.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("Field.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + Field[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < Field.Length; ++i)
        {
            query += " AND " + Field[i] + operation[i] + "'" + values[0] + "' ";
        }

        return ExecuteQuery(query);
    }

}