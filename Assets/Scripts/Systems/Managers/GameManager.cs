using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{
	public List<Player> curPlayerUnits = new(); //현재 게임에 스폰되어 있는 플레이어 유닛List
	public List<Enemy> curEnemyUnits = new(); //현재 게임에 스폰되어 있는 적 유닛List

	public PlayerBase playerBase;
	public EnemyBase enemyBase;

	public PlayerBaseSpawner playerBaseSpawner;

	private CoinManager coinManager;

	public CoinManager getCoinManager() { return coinManager; }
	public void setCoinManager(CoinManager coinManager) { this.coinManager = coinManager; }

	private void Update()
	{
		if (Input.GetKey(KeyCode.Q)) DoubleSpeedTime();
		else if (Input.GetKey(KeyCode.W)) Resume();
	}

	public void CanLevelUp()
	{
		if (coinManager.level >= coinManager.maxCoinByLevel.Length - 1) return;

		if (coinManager.curCoin < coinManager.costByLevelUp[coinManager.level])
		{
			return;
		}
		else
		{
			coinManager.curCoin -= coinManager.costByLevelUp[coinManager.level];
			LevelUp();
		}
	}

	//레벨별 최대 코인의 길이보다 레벨이 크거나 같다면 레벨이 초과했으니 return.
	//레벨업 버튼을 눌렀는데 현재 가진 코인이 레벨업 하기 위한 자원보다 작은 경우.
	//현재 가진 코인이 레벨업 하기 위한 자원보다 클 경우 레벨업.


	//레벨업 메서드.
	private void LevelUp()
	{
		coinManager.level++;

		if (coinManager.level == coinManager.maxCoinByLevel.Length - 1)
		{
			UIManager.Instance.MaxLevel();
			return;
		}
		UIManager.Instance.UpdateLevelText();
		UIManager.Instance.UpdateCostText();
	}

	//게임 진행
	public void Resume()
	{
		Time.timeScale = 1;
	}

	//일시 정지
	public void Stop()
	{
		Time.timeScale = 0;
	}

	public void DoubleSpeedTime()
	{
		Time.timeScale = 10;
	}

	//타이틀 씬 불러오기
	public void LoadTitleScene()
	{
		Init();
		MySceneManager.Instance.LoadTitleScene();
	}

	//게임 매니저 관련 필드 초기화
	public void Init()
	{
		curPlayerUnits.Clear();
		curEnemyUnits.Clear();
		Resume();
	}

	//GameManager에 있는 curPlayerUnits에 Add할 떄 리스트안에서 y좌표 별로 정렬해서 넣어놔야 함.
	//높은 y좌표 값부터 내림차순 정렬. 그리고 그 정렬된 List에 있는 요소 Layer값을 순차적으로 +1씩 부여.
	public void DescendingSortList<T>(List<T> list) where T : MonoBehaviour
	{
		List<T> u_List = new List<T>();

		while (u_List.Count < list.Count)
		{
			float maxY = float.MinValue;
			T bigUnit = null;

			foreach (T unit in list)
			{
				// 이미 추가된 플레이어는 건너뛰기
				if (u_List.Contains(unit)) continue;

				// y값이 가장 큰 플레이어 찾기
				if (unit.transform.position.y > maxY)
				{
					maxY = unit.transform.position.y;
					bigUnit = unit;
				}
			}

			if (bigUnit != null)
			{
				u_List.Add(bigUnit);
			}
		}

		// 정렬된 리스트를 curPlayerUnits에 업데이트
		for (int i = 0; i < u_List.Count; i++)
		{
			list[i] = u_List[i];
			list[i].GetComponent<SortingGroup>().sortingOrder = i; // 순차적으로 레이어 값 부여
		}
	}
}
