using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Base
{
    private void Awake()
    {
        maxHealth = health;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.UpdatePlayerBaseHeathText(); //플레이어 기지 체력Text 업뎃


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
        GameManager.Instance.Stop();
    }
}
