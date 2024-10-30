using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private void Update()
    {
        //적이 있다면 if문 실행
        if (isTarget)
        {
            //공격이 가능하다면 공격.
            if (CanAttack()) Attack();
        }
    }
    private void FixedUpdate()
    {
        //타겟이 없다면 움직이기
        if (isTarget == false) Move(Vector2.right);

        TargetScan(Vector2.right);
    }
}
