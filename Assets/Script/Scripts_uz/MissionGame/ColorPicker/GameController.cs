using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Color[] colorPalette;   //색상목록
    [SerializeField]
    private float difficultModifier;    // 색상이 다른 정도(높을수록 더 달라보임)
    [SerializeField][Range(2,5)]
    private int blockCount = 2;      //블록개수
    [SerializeField]
    private BlockSpawner blockSpawner;

    // 생성한 모든 블록의 정보를 가지고 있는 리스트
    private List<Block>   blockList= new List<Block>();

    private Color   currentColor; //현재 블록들의 색상
    private Color   otherOneColor; //하나의 블록에 적용하는 살짝 다른 색상
    
    private int     otherBlockIndex; //색상이 다른 하나의 블록 인덱스
   
    private void Awake(){
        blockList= blockSpawner.SpawnBlocks(blockCount);
        for(int i=0; i<blockList.Count; ++i){
            blockList[i].Setup(this);
        }
        SetColors();
    }

    private void SetColors(){
        //블록의 색이 바뀔때마다 정답 블록의 색상이 다른 블록들과 더 비슷한 색상으로 보이도록 값 감소 (난이도 높아짐)
        difficultModifier *= 0.92f;

        // 기본 블록들의 색상
        Color currentColor = colorPalette[Random.Range(0, colorPalette.Length)];
        
        // 정답 블록의 색상
        float diff = (1.0f/255.0f) * difficultModifier;
        otherOneColor = new Color(currentColor.r-diff, currentColor.g-diff, currentColor.b-diff);

        // 정답 블록의 순번
        otherBlockIndex = Random.Range(0, blockList.Count);
        Debug.Log(otherBlockIndex); // 정답블록 인덱스를 콘솔창에 출력. 게임 완성후 삭제

        // 블록 색상 설정
        for(int i = 0; i<blockList.Count; ++i){

            if(i==otherBlockIndex){
                blockList[i].Color=otherOneColor;
            } else{
                blockList[i].Color=currentColor;
            }
        }
    }

    public void CheckBlock(Color color){
        // 색상이 다른 하나의 블록과 매개변수 color의 색상이 같으면
        // 플레이어가 선택한 블록이 정답 블록 = 정답
        if(blockList[otherBlockIndex].Color == color){
            //색상 재선택
            SetColors();
            Debug.Log("색상 일치, 점수 획득 등의 처리 ..");
        } else{
            Debug.Log("실패");
            UnityEditor.EditorApplication.ExitPlaymode();
        }
    }
}
