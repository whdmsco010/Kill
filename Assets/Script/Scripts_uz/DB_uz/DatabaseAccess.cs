using UnityEngine;
using Mono.Data.Sqlite;

public class DatabaseAccess
{
    private SqliteConnection m_DatabaseConnection;
    private SqliteCommand m_DatabaseCommand;
    private SqliteDataReader m_Reader;

    public DatabaseAccess(string connectionString)
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

    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + cols[0];
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    // 업데이트 하는 항목이 문자열이 아닐수도 있는데 그럴경우는 어떻게 해야하는가?
    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, int selectvalue)
    {

        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }

    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
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

    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }

        return ExecuteQuery(query);
    }
}