using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Base : MonoBehaviour, ITakeDamage
{
    public int health;
    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        print($"{this.name}�� �������� �԰� ����. ���� ü��: {health}/{maxHealth}");
        if (health <= 0)
        {
            health = 0;
            //������ �μ���
            print($"{this.name}�� �μ���");
            //�¸�/�й� UI ȣ��
        }
    }
}
