using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ������ �⺻ �Ӽ�, �޼ҵ�
public class Unit : MonoBehaviour
{
    [Tooltip("���� ���� ������ �Դϴ�.")]
    [SerializeField] private UnitData unitData;
    internal int health; //ü��
    internal int damage; //���ݷ�
    internal float attackInterval; //���ݼӵ�
    internal int attackRange; //���� ���� �Ÿ�
    internal int moveSpeed; //�̵� �ӵ�
    internal int cost; //���� ���
    internal bool isTarget = false; //���� �����Ǹ� ���߱�
    private void Start()
    {
        //���� �ʱ�ȭ
        health = unitData.health;
        damage = unitData.damage;
        attackInterval = unitData.attackInterval;
        attackRange = unitData.attackRange;
        moveSpeed = unitData.moveSpeed;
        cost = unitData.cost;
    }

    //������ �޴� �޼���
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //�׾��� �� ó��
            GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
