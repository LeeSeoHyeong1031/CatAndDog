using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private void Update()
    {
        //���� �ִٸ� if�� ����
        if (isTarget)
        {
            //������ �����ϴٸ� ���� �Ÿ� �ȿ� �ִ� colls ��� Attack().
            if (CanAttack()) Attack();
        }
    }
    private void FixedUpdate()
    {
        //Ÿ���� ���ٸ� �����̱�
        if (isTarget == false) Move(Vector2.left);

        //���� �����ϰ� ������ �Ǹ� isTarget = true
        TargetScan(Vector2.left);
    }
}
