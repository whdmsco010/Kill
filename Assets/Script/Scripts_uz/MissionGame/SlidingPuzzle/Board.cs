using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField]
	private	GameObject	tilePrefab;								// 숫자 타일 프리팹
	[SerializeField]
	private	Transform	tilesParent;							// 타일이 배치되는 "Board" 오브젝트의 Transform

	private	List<PuzzleTile>	tileList;								// ������ Ÿ�� ���� ����

	private	Vector2Int	puzzleSize = new Vector2Int(4, 4);		// 4x4 퍼즐
	private	float		neighborTileDistance = 102;				// ������ Ÿ�� ������ �Ÿ�. ������ ����� ���� �ִ�.

	public	Vector3		EmptyTilePosition { set; get; }			// �� Ÿ���� ��ġ
	public	int			Playtime { private set; get; } = 0;		// ���� �÷��� �ð�
	public	int			MoveCount { private set; get; } = 0;	// �̵� Ƚ��

	
	private IEnumerator Start()
	{
		tileList = new List<PuzzleTile>();

		SpawnTiles();


		UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());

		// ���� �������� ����� ������ ���
		yield return new WaitForEndOfFrame();

		// tileList�� �ִ� ��� ����� SetCorrectPosition() �޼ҵ� ȣ��
		tileList.ForEach(x => x.SetCorrectPosition());

		StartCoroutine("OnSuffle");
		// ���ӽ��۰� ���ÿ� �÷��̽ð� �� ���� ����
		StartCoroutine("CalculatePlaytime");
		
	}
	
	private void SpawnTiles()
	{
		for ( int y = 0; y < puzzleSize.y; ++ y )
		{
			for ( int x = 0; x < puzzleSize.x; ++ x )
			{
				GameObject clone = Instantiate(tilePrefab, tilesParent);
				PuzzleTile tile = clone.GetComponent<PuzzleTile>();

				tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

				tileList.Add(tile);
			}
		}
	}
	
	private IEnumerator OnSuffle()
	{
		float current	= 0;
		float percent	= 0;
		float time		= 1.5f;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / time;

			int index = Random.Range(0, puzzleSize.x * puzzleSize.y);
			tileList[index].transform.SetAsLastSibling();

			yield return null;
		}

		// ���� ���� ����� �ٸ� ����̾��µ� UI, GridLayoutGroup�� ����ϴٺ��� �ڽ��� ��ġ�� �ٲٴ� ������ ����
		// �׷��� ���� Ÿ�ϸ���Ʈ�� �������� �ִ� ��Ұ� ������ �� Ÿ��
		EmptyTilePosition = tileList[tileList.Count-1].GetComponent<RectTransform>().localPosition;
	}

	public void IsMoveTile(PuzzleTile tile)
	{
		if ( Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
		{
			Vector3 goalPosition = EmptyTilePosition;

			EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;

			tile.OnMoveTo(goalPosition);

			// Ÿ���� �̵��� ������ �̵� Ƚ�� ����
			MoveCount ++;
		}
	}

	public void IsGameOver()
	{
		List<PuzzleTile> tiles = tileList.FindAll(x => x.IsCorrected == true);

		Debug.Log("Correct Count : "+tiles.Count);
		if ( tiles.Count == puzzleSize.x * puzzleSize.y - 1 )
		{
			Debug.Log("GameClear");
			// ���� Ŭ�������� �� �ð���� ����
			StopCoroutine("CalculatePlaytime");
			// Board ������Ʈ�� ������Ʈ�� �����ϱ� ������
			// �׸��� �ѹ��� ȣ���ϱ� ������ ������ ������ �ʰ� �ٷ� ȣ��..
			GetComponent<UIController>().OnResultPanel();
		}
	}

	private IEnumerator CalculatePlaytime()
	{
		while ( true )
		{
			Playtime ++;

			yield return new WaitForSeconds(1);
		}
	}
	
}
