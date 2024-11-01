using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private Animator anim;

    private void Awake()
    {
        Init(); //���� �ʱ�ȭ
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        ScaleCtrl(); //localeScale ����
    }
    private void Update()
    {
        if (dead) { return; }

        //���� �ִٸ� if�� ����
        if (isTarget == true)
        {
            //������ �����ϴٸ� ���� �Ÿ� �ȿ� �ִ� colls ��� Attack().
            if (CanAttack())
            {
                anim.SetTrigger("2_Attack");
                Attack();
            }
        }
    }
    private void FixedUpdate()
    {
        if (dead) { return; }

        //Ÿ���� ���ٸ� �����̱�
        if (isTarget == false)
        {
            Move(Vector2.right);
            anim.SetBool("1_Move", true);
        }
        else anim.SetBool("1_Move", false);

        //���� �����ϰ� ������ �Ǹ� isTarget = true
        TargetScan(Vector2.right);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        //anim.SetTrigger("3_Damaged");
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        //�ִϸ��̼� ó�� �κ�
        anim.SetTrigger("4_Death");
        anim.SetBool("isDeath", true);

        GameManager.Instance.curEnemyUnits.Remove(this);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}