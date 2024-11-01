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
        UIManager.Instance.UpdateEnemyBaseHeathText(); //�� ���� ü��Text ����


        //������ �ѹ� �ı��Ǹ� ���̴� ü���� 0�̰� ���� �ʾҴٸ� ����. �� �ѹ��� ����.
        if (health <= 0 && dead == false)
        {
            health = 0;
            dead = true;
            //�¸�/�й� UI ȣ��
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