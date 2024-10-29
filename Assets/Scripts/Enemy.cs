using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    void Update()
    {
        //타겟이 없다면 움직이기
        if (isTarget == false) Move();
    }

    public void Move()
    {
        transform.Translate(moveSpeed * Vector2.right * Time.deltaTime);
    }

    //콜라이더 내 적이 있다면 데지미 주기.
    private void OnTriggerStay2D(Collider2D collision)
    {
        //범위 내 들어 오면 멈춰야 하니 isTarget = true
        isTarget = true; // 멈추기
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        //TODO:닿으면 바로 죽음. 왜냐하면 초당 24프레임이 찍히니까. 코루인 WaitForSeconds 써서 초당 데미지 추가하기
    }
}
