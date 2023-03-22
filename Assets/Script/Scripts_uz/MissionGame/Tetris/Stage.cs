using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    //필요 소스 불러오기
    [Header("Source")]
    public GameObject tilePrefab;
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;

    public GameObject gameoverPanel;
    public Text score;
    public Text level;
    public Text line;

    [Header("Setting")]
    [Range(4, 40)]
    public int boardWidth = 10;
    //높이 설정
    [Range(5, 20)]
    public int boardHeight = 20;
    //떨어지는 속도
    public float fallCycle = 1.0f;
    //위치 보정
    public float offset_x = 0f;
    public float offset_y = 0f;

    private int halfWidth;
    private int halfHeight;

    private float nextFallTime; // 다음에 테트로미노가 떨어질 시간을 저장

    // UI 관련 변수
    private int scoreVal = 0;
    private int levelVal = 1;
    private int lineVal;


    private void Start() // 게임이 시작되고 한 번만 실행
    {
        // 게임 시작시 text 설정
        lineVal = levelVal * 2;   // 임시 레벨 디자인
        score.text = "" + scoreVal;
        level.text = "" + levelVal;
        line.text = "" + lineVal;

        // 게임 시작시 게임오버 패널 off
        gameoverPanel.SetActive(false);

        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);    // 너비의 중간값 설정해주기
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);   // 높이의 중간값 설정해주기

        nextFallTime = Time.time + fallCycle;   // 다음에 테트로미노가 떨어질 시간 설정

        CreateBackground(); // 배경 만들기

        // 높이만큼 행 노드 만들어주기
        for (int i = 0; i < boardHeight; ++i)
        {
            // ToString을 이용하여 오브젝트 이름 설정
            var col = new GameObject((boardHeight - i - 1).ToString());
            // 위치설정 -> 행 위치의 높이, 가로 중앙
            col.transform.position = new Vector3(0, halfHeight - i, 0);
            // 보드 노드의 자식으로 설정
            col.transform.parent = boardNode;
        }

        CreateTetromino(); // 테트로미노 만들기
    }


    void Update()   // 매 프레임마다 실행
    {
        // 게임오버 처리
        if (gameoverPanel.activeSelf)
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(0);
            }
        }
        // 게임 처리
        else
        {
            //초기화
            Vector3 moveDir = Vector3.zero; //이동 여부 저장용
            bool isRotate = false;  // 회전 여부 저장용

            //각 키에 따라 이동 여부 혹은 회전 여부를 설정해줍니다.
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
                // 테트로미노가 바닥에 닿을 때까지 아래로 이동
                while (MoveTetromino(Vector3.down, false))
                {
                }
            }

            if (Input.GetKeyDown("r"))
            {
                // SceneManager을 이용하여 게임 재시작하기
                // 가장 위에 using UnityEngine.SceneManagement; 추가 필요
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // 아래로 떨어지는 경우는 강제로 이동시킵니다.
            if (Time.time > nextFallTime)
            {
                nextFallTime = Time.time + fallCycle;   // 다음 떨어질 시간 재설정
                moveDir.y = -1; //  아래로 한 칸 이동
                isRotate = false;   // 강제로 이동시 회전 없음
            }

            // 아무런 키 입력이 없을경우 Tetromino 움직이지 않게하기
            if (moveDir != Vector3.zero || isRotate)
            {
                MoveTetromino(moveDir, isRotate);
            }
        }
    }

    // 이동이 가능하면 true, 불가능하면 false를 return
    bool MoveTetromino(Vector3 moveDir, bool isRotate)
    {
        //이동 or 회전 불가시 돌아가기 위한 값 저장
        Vector3 oldPos = tetrominoNode.transform.position;
        Quaternion oldRot = tetrominoNode.transform.rotation;

        //이동 & 회전 하기
        tetrominoNode.transform.position += moveDir;
        if (isRotate)
        {
            //현재 테트로미노 노드에 90도 회전을 더해 줌.
            tetrominoNode.transform.rotation *= Quaternion.Euler(0, 0, 90);
        }

        // 이동 불가시 이전 위치, 회전 으로 돌아가기
        if (!CanMoveTo(tetrominoNode))
        {
            tetrominoNode.transform.position = oldPos;
            tetrominoNode.transform.rotation = oldRot;

            //바닥에 닿았다는 의미 = 이동 불가하고 현재 아래로 떨어지고 있는 상황
            if ((int)moveDir.y == -1 && (int)moveDir.x == 0 && isRotate == false)
            {
                AddToBoard(tetrominoNode);
                CheckBoardColumn();
                CreateTetromino();

                //테트로미노 새로 추가 직후 이동 가능 확인
                if (!CanMoveTo(tetrominoNode))
                {
                    gameoverPanel.SetActive(true);
                }
            }

            return false;
        }

        return true;
    }

    // 테트로미노를 보드에 추가
    void AddToBoard(Transform root)
    {
        while (root.childCount > 0)
        {
            var node = root.GetChild(0);

            //유니티 좌표계에서 테트리스 좌표계로 변환
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);

            //부모노드 : 행 노드(y 위치), 오브젝트 이름 : x 위치
            node.parent = boardNode.Find(y.ToString());
            node.name = x.ToString();
        }
    }

    // 보드에 완성된 행이 있으면 삭제
    void CheckBoardColumn()
    {
        bool isCleared = false;
        //한번에 사라진 행 개수 확인용
        int linecount = 0;

        // 완성된 행 == 행의 자식 갯수가 가로 크기
        foreach (Transform column in boardNode)
        {
            if (column.childCount == boardWidth)
            {
                //행의 모든 자식을 삭제
                foreach (Transform tile in column)
                {
                    Destroy(tile.gameObject);
                }
                // 행의 모든 자식들과의 연결 끊기
                column.DetachChildren();
                isCleared = true;
                linecount++; // 완성된 행 하나당 linecount 증가
            }
        }

        // 완성된 행이 있을경우 점수증가
        if (linecount != 0)
        {
            scoreVal += linecount * linecount * 100;
            score.text = "" + scoreVal;
        }

        // 완성된 행이 있을경우 남은라인 감소
        if (linecount != 0)
        {
            lineVal -= linecount;
            // 레벨업까지 필요 라인 도달경우 (최대 레벨 10으로 한정)
            if (lineVal <= 0 && levelVal <= 10)
            {
                levelVal += 1;  // 레벨증가
                lineVal = levelVal * 2 + lineVal;   // 남은 라인 갱신
                fallCycle = 0.1f * (10 - levelVal); // 속도 증가
            }
            level.text = "" + levelVal;
            line.text = "" + lineVal;
        }

        // 비어 있는 행이 존재하면 아래로 내리기
        if (isCleared)
        {
            //가장 바닥 행은 내릴 필요가 없으므로 index 1 부터 for문 시작
            for (int i = 1; i < boardNode.childCount; ++i)
            {
                var column = boardNode.Find(i.ToString());

                // 이미 비어 있는 행은 무시
                if (column.childCount == 0)
                    continue;

                // 현재 행 아래쪽에 빈 행이 존재하는지 확인, 빈 행만큼 emptyCol 증가
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

                // 현재 행 아래쪽에 빈 행 존재시 아래로 내림
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


    // 이동 가능한지 체크 후 True or False 반환하는 메서드
    bool CanMoveTo(Transform root)  //tetrominoNode를 매개변수 root로 가져오기
    {
        // tetrominoNode의 자식 타일을 모두 검사
        for (int i = 0; i < root.childCount; ++i)
        {
            var node = root.GetChild(i);
            // 유니티 좌표계에서 테트리스 좌표계로 변환
            int x = Mathf.RoundToInt(node.transform.position.x + halfWidth);
            int y = Mathf.RoundToInt(node.transform.position.y + halfHeight - 1);
            // 이동 가능한 좌표인지 확인 후 반환
            if (x < 0 || x > boardWidth - 1)
                return false;
            if (y < 0)
                return false;

            // 이미 다른 타일이 있는지 확인
            var column = boardNode.Find(y.ToString());

            if (column != null && column.Find(x.ToString()) != null)
                return false;
        }
        return true;
    }


    // 타일 생성
    Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1)
    {
        var go = Instantiate(tilePrefab); // tilePrefab를 복제한 오브젝트 생성
        go.transform.parent = parent; // 부모 지정
        go.transform.localPosition = position; // 위치 지정

        var tile = go.GetComponent<Tile>(); // tilePrefab의 Tile스크립트 불러오기
        tile.color = color; // 색상 지정
        tile.sortingOrder = order; // 순서 지정

        return tile;
    }

    // 배경 타일을 생성
    void CreateBackground()
    {
        Color color = Color.gray;   // 배경 색 설정(원하는 색으로 설정 가능)

        // 타일 보드
        color.a = 0.5f; // 테두리와 구분 위해 투명도 바꾸기
        for (int x = -halfWidth; x < halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), color, 0);
            CreateTile(backgroundNode, new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            CreateTile(backgroundNode, new Vector2(x, -halfHeight), color, 0);
        }
    }


    // 테트로미노 생성
    void CreateTetromino()
    {
        int index = Random.Range(0, 7); // 랜덤으로 0~6 사이의 값 생성
        //위의 코드 대신 아래와 같은 코드로 원하는 Index모양 확인 가능
        //int index = 1; 

        Color32 color = Color.white;

        // 회전 계산에 사용하기 위한 쿼터니언 클래스
        tetrominoNode.rotation = Quaternion.identity;
        // 테트로미노 생성 위치 (중앙 상단)   
        tetrominoNode.position = new Vector2(offset_x, halfHeight + offset_y);

        switch (index)
        {
            // 구분을 위해 테트리스 모양에 비슷하게 영어로 표현 (I, J, L ,O, S, T, Z)

            case 0: // I
                color = new Color32(115, 251, 253, 255);    // 하늘색
                CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                break;

            case 1: // J
                color = new Color32(0, 33, 245, 255);    // 파란색
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color);
                break;

            case 2: // L
                color = new Color32(243, 168, 59, 255);    // 주황색
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color);
                break;

            case 3: // O 
                color = new Color32(255, 253, 84, 255);    // 노란색
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1f), color);
                break;

            case 4: //  S
                color = new Color32(117, 250, 76, 255);    // 녹색
                CreateTile(tetrominoNode, new Vector2(-1f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                break;

            case 5: //  T
                color = new Color32(155, 47, 246, 255);    // 자주색
                CreateTile(tetrominoNode, new Vector2(-1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                break;

            case 6: // Z
                color = new Color32(235, 51, 35, 255);    // 빨간색
                CreateTile(tetrominoNode, new Vector2(-1f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                break;
        }
    }

}
