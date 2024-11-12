using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseSpawner : BaseSpawner
{
	public Enemy n_goblin;
	public Enemy r_goblin;
	public Enemy t_goblin;

	private void Start()
	{
		n_goblin.lastSpawnTime = Time.time + n_goblin.startSpawnTime;
		r_goblin.lastSpawnTime = Time.time + r_goblin.startSpawnTime;
		t_goblin.lastSpawnTime = Time.time + t_goblin.startSpawnTime;
	}

	private void Update()
	{
		SpawnTimeCheck(n_goblin);
		SpawnTimeCheck(r_goblin);
		SpawnTimeCheck(t_goblin);
	}

	public void SpawnTimeCheck(Enemy enemy)
	{
		if (Time.time >= enemy.lastSpawnTime)
		{
			enemy.lastSpawnTime = Time.time + enemy.spawnInterval;
			Spawn(enemy);
		}
	}

	public override void Spawn(Unit unit)
	{
		Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(MIN_DISTANCE, MAX_DISTANCE), 0);
		Enemy enemy = Instantiate(unit, spawnPoint, Quaternion.identity) as Enemy;

		GameManager.Instance.curEnemyUnits.Add(enemy);
		GameManager.Instance.DescendingSortList(GameManager.Instance.curEnemyUnits);
	}
}
