using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Base
{
    private Animator anim;

    private void Awake()
    {
        maxHealth = health;
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.UpdateEnemyBaseHeathText(); //적 기지 체력Text 업뎃


        //기지는 한번 파괴되면 끝이니 체력이 0이고 죽지 않았다면 실행. 즉 한번만 실행.
        if (health <= 0 && dead == false)
        {
            health = 0;
            dead = true;
            //승리/패배 UI 호출
            GameOver();
        }
    }
    public void GameOver()
    {
        anim.SetTrigger("Die");
        anim.SetBool("isDeath", true);
        GameManager.Instance.Stop();
    }
}
