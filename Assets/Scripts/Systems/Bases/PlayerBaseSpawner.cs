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

    //�÷��̾� ���� ����
    public override void Spawn(Unit unit)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isSpawning");
            Vector3 spawnPoint = new Vector3(transform.position.x, Random.Range(MIN_DISTANCE, MAX_DISTANCE), 0);
            Player player = Instantiate(unit, spawnPoint, Quaternion.identity) as Player; //UnitŸ���̴� PlayerŸ������ ���� ����ȯ.
            GameManager.Instance.curPlayerUnits.Add(player);

            GameManager.Instance.DescendingSortList(GameManager.Instance.curPlayerUnits);
        }
    }
}
