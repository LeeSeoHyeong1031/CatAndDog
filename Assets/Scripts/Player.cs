using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private void Update()
    {
        //적이 있다면 if문 실행
        if (isTarget)
        {
            //공격이 가능하다면 공격 거리 안에 있는 colls 모두 Attack().
            if (CanAttack()) Attack();
        }
    }
    private void FixedUpdate()
    {
        //타겟이 없다면 움직이기
        if (isTarget == false) Move(Vector2.left);

        //적을 감지하고 감지가 되면 isTarget = true
        TargetScan(Vector2.left);
    }
}
