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
        UIManager.Instance.UpdatePlayerBaseHeathText(); //�÷��̾� ���� ü��Text ����


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
        GameManager.Instance.Stop();
    }
}
