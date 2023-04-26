using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using Debug = UnityEngine.Debug;

public class Change : MonoBehaviour
{
    public int sceneIndex;
    public GameObject targetObject;

    private DB_Controller m_DatabaseAccess;

    void OnMouseDown()
    {
        OpenDB("DB_DO.db");

        Vector3 pos;
        pos = this.targetObject.transform.position;
        m_DatabaseAccess.SqlQuery("UPDATE DB_DO SET Location_x = " + pos.x + " WHERE Player = 3");
        m_DatabaseAccess.SqlQuery("UPDATE DB_DO SET Location_y = " + pos.y + " WHERE Player = 3");

        Debug.Log(pos.x);
        Debug.Log(pos.y);
        SceneManager.LoadScene(sceneIndex);
    }
    
    void OpenDB(string DBFileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, DBFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DB_Controller("data source = " + filePath);
    }
}
