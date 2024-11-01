using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseSpawner : BaseSpawner
{
    public Player n_solider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Spawn(n_solider);
    }

    //플레이어 유닛 스폰
    public override void Spawn(Unit unit)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isSpawning");
            Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(MIN_DISTANCE, MAX_DISTANCE), 0);
            Player player = Instantiate(unit, spawnPoint, Quaternion.identity) as Player; //Unit타입이니 Player타입으로 강제 형변환.
            GameManager.Instance.curPlayerUnits.Add(player);

            GameManager.Instance.DescendingSortList(GameManager.Instance.curPlayerUnits);
        }
    }
}
