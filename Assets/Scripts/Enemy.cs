using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private void Update()
    {
        //���� �ִٸ� if�� ����
        if (isTarget)
        {
            //������ �����ϴٸ� ����.
            if (CanAttack()) Attack();
        }
    }
    private void FixedUpdate()
    {
        //Ÿ���� ���ٸ� �����̱�
        if (isTarget == false) Move(Vector2.right);

        TargetScan(Vector2.right);
    }
}
