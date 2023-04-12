using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{

    [Header("Source")]
    public GameObject tilePrefab;
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;

    public GameObject gameoverPanel;
    public GameObject gameClearPanel;
    public Text score;
    public Text level;
    public Text line;

    [Header("Setting")]
    [Range(4, 40)]
    public int boardWidth = 10;

    [Range(5, 20)]
    public int boardHeight = 20;

    public float fallCycle = 1.0f;

    public float offset_x = 0f;
    public float offset_y = 0f;

    private int halfWidth;
    private int halfHeight;

    private float nextFallTime; 

   
    private int scoreVal = 0;
    private int levelVal = 1;
    private int lineVal;


    private void Start() 
    {
       
        lineVal = levelVal * 2;   
        score.text = "" + scoreVal;
        level.text = "" + levelVal;
        line.text = "" + lineVal;

        
        gameoverPanel.SetActive(false);

        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);    
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);   

        nextFallTime = Time.time + fallCycle;   

        CreateBackground(); 

        
        for (int i = 0; i < boardHeight; ++i)
        {
            
            var col = new GameObject((boardHeight - i - 1).ToString());
            
            col.transform.position = new Vector3(0, halfHeight - i, 0);
            
            col.transform.parent = boardNode;
        }

        CreateTetromino(); 
    }


    void Update()   
    {

        if (gameoverPanel.activeSelf)
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(0);
            }
        }
        
        else
        {
           
            Vector3 moveDir = Vector3.zero; 
            bool isRotate = false;  

            
            if (Input.GetKeyDown("a"))
            {
                moveDir.x = -1;

            }
            else if (Input.GetKeyDown("d"))
            {
                moveDir.x = 1;
            }

            if (Input.GetKeyDown("w"))
            {
                isRotate = true;
            }
            else if (Input.GetKeyDown("s"))
            {
                moveDir.y = -1;
            }


            if (Input.GetKeyDown("space"))
            {
                
                while (MoveTetromino(Vector3.down, false))
                {
                }
            }

            if (Input.GetKeyDown("r"))
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            
            if (Time.time > nextFallTime)
            {
                nextFallTime = Time.time + fallCycle;   
                moveDir.y = -1; 
                isRotate = false;   
            }

            
            if (moveDir != Vector3.zero || isRotate)
            {
                MoveTetromino(moveDir, isRotate);
            }
        }
    }


    bool MoveTetromino(Vector3 moveDir, bool isRotate)
    {
   
        Vector3 oldPos = tetrominoNode.transform.position;
        Quaternion oldRot = tetrominoNode.transform.rotation;


        tetrominoNode.transform.position += moveDir;
        if (isRotate)
        {

            tetrominoNode.transform.rotation *= Quaternion.Euler(0, 0, 90);
        }


        if (!CanMoveTo(tetrominoNode))
        {
            tetrominoNode.transform.position = oldPos;
            tetrominoNode.transform.rotation = oldRot;


            if ((int)moveDir.y == -1 && (int)moveDir.x == 0 && isRotate == false)
            {
                AddToBoard(tetrominoNode);
                CheckBoardColumn();
                CreateTetromino();

                if (!CanMoveTo(tetrominoNode))
                {
                    gameoverPanel.SetActive(true);
                }
            }

            return false;
        }

        return true;
    }


    void AddToBoard(Transform root)
    {
        while (root.childCount > 0)
        {
            var node = root.GetChild(0);


            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);


            node.parent = boardNode.Find(y.ToString());
            node.name = x.ToString();
        }
    }


    void CheckBoardColumn()
    {
        bool isCleared = false;

        int linecount = 0;


        foreach (Transform column in boardNode)
        {
            if (column.childCount == boardWidth)
            {

                foreach (Transform tile in column)
                {
                    Destroy(tile.gameObject);
                }
      
                column.DetachChildren();
                isCleared = true;
                linecount++; 
            }
        }

        if (linecount != 0)
        {
            scoreVal += linecount * linecount * 100;
            score.text = "" + scoreVal;
            if(scoreVal>=1000){
                Debug.Log("Clear");
                gameClearPanel.SetActive(true);

            }
        }


        if (linecount != 0)
        {
            lineVal -= linecount;

            if (lineVal <= 0 && levelVal <= 10)
            {
                levelVal += 1;  
                lineVal = levelVal * 2 + lineVal;   
                fallCycle = 0.1f * (10 - levelVal);
            }
            level.text = "" + levelVal;
            line.text = "" + lineVal;
        }

        if (isCleared)
        {

            for (int i = 1; i < boardNode.childCount; ++i)
            {
                var column = boardNode.Find(i.ToString());


                if (column.childCount == 0)
                    continue;

                int emptyCol = 0;
                int j = i - 1;
                while (j >= 0)
                {
                    if (boardNode.Find(j.ToString()).childCount == 0)
                    {
                        emptyCol++;
                    }
                    j--;
                }


                if (emptyCol > 0)
                {
                    var targetColumn = boardNode.Find((i - emptyCol).ToString());

                    while (column.childCount > 0)
                    {
                        Transform tile = column.GetChild(0);
                        tile.parent = targetColumn;
                        tile.transform.position += new Vector3(0, -emptyCol, 0);
                    }
                    column.DetachChildren();
                }
            }
        }
    }

    bool CanMoveTo(Transform root)  
    {
        
        for (int i = 0; i < root.childCount; ++i)
        {
            var node = root.GetChild(i);
       
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);
          
            if (x < 0 || x > boardWidth - 1)
                return false;
            if (y < 0)
                return false;

      
            var column = boardNode.Find(y.ToString());

            if (column != null && column.Find(x.ToString()) != null)
                return false;
        }
        return true;
    }


   
    Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1)
    {
        var go = Instantiate(tilePrefab); 
        go.transform.parent = parent;
        go.transform.localPosition = position; 

        var tile = go.GetComponent<Tile>(); 
        tile.color = color; 
        tile.sortingOrder = order; 

        return tile;
    }

   
    void CreateBackground()
    {
        Color color = Color.gray;   

       
        color.a = 0.5f; 
        for (int x = -halfWidth; x < halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

      
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), color, 0);
            CreateTile(backgroundNode, new Vector2(halfWidth, y), color, 0);
        }

      
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            CreateTile(backgroundNode, new Vector2(x, -halfHeight), color, 0);
        }
    }


  
    void CreateTetromino()
    {
        int index = Random.Range(0, 7); 

        Color32 color = Color.white;

        tetrominoNode.rotation = Quaternion.identity;

        tetrominoNode.position = new Vector2(offset_x, halfHeight + offset_y);

        switch (index)
        {

            case 0: // I
                color = new Color32(115, 251, 253, 255);   
                CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                break;

            case 1: // J
                color = new Color32(0, 33, 245, 255);   
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color);
                break;

            case 2: // L
                color = new Color32(243, 168, 59, 255);   
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color);
                break;

            case 3: // O 
                color = new Color32(255, 253, 84, 255);   
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1f), color);
                break;

            case 4: //  S
                color = new Color32(117, 250, 76, 255);  
                CreateTile(tetrominoNode, new Vector2(-1f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                break;

            case 5: //  T
                color = new Color32(155, 47, 246, 255);   
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                break;

            case 6: // Z
                color = new Color32(235, 51, 35, 255);   
                CreateTile(tetrominoNode, new Vector2(-1f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                break;
        }
    }

}
