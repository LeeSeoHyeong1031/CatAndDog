using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseSpawner : BaseSpawner
{
    public Enemy n_goblin;
    public float n_SpawnInterval = 5f; //일반몹 스폰 시간

    private void Start()
    {
        StartCoroutine(N_AutoSpawn());
    }
    public IEnumerator N_AutoSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(n_SpawnInterval);
            Spawn(n_goblin);
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
