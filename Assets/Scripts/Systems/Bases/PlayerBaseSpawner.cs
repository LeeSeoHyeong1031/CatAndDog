using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseSpawner : BaseSpawner
{
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	//������ ��������
	public void CanSpawn(Player player)
	{
		//���� ���� ������ ������ ������ ��뺸�� ���ٸ� ����.
		if (GameManager.Instance.curCoin >= player.unitData.cost)
		{
			GameManager.Instance.curCoin -= player.unitData.cost;
			Spawn(player);
		}
	}

	//�÷��̾� ���� ����
	public override void Spawn(Unit unit)
	{
		anim.SetTrigger("isSpawning");
		Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(MIN_DISTANCE, MAX_DISTANCE), 0);
		Player player = Instantiate(unit, spawnPoint, Quaternion.identity) as Player; //UnitŸ���̴� PlayerŸ������ ���� ����ȯ.
		GameManager.Instance.curPlayerUnits.Add(player);

		GameManager.Instance.DescendingSortList(GameManager.Instance.curPlayerUnits);
	}
}
