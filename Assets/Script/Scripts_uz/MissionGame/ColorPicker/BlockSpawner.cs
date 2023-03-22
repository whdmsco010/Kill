using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block  blockPrefab;        //블록 프리팹
    [SerializeField]
    private GridLayoutGroup gridLayout;     // GridLayoutGroup Component

    public List<Block> SpawnBlocks(int blockCount){

        List<Block> blockList = new List<Block>(blockCount * blockCount);
        // 셀 크기
        int cellSize = 300 - 50 *(blockCount-2);
        gridLayout.cellSize = new Vector2(cellSize,cellSize);   //x,y
        // 가로에 배치되는 셀 개수
        gridLayout.constraintCount = blockCount;

        // blockCount * blockCount 개수만큼 블록을 생성
        for(int y=0; y < blockCount; ++y){
            for(int x=0; x<blockCount; ++x){
                Block block = Instantiate(blockPrefab,gridLayout.transform);
                blockList.Add(block);
            }
        }
        return blockList;

    }

}
