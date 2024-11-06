using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TitlePlayer : Unit
{
    private Animator anim;
    public float cooltime;

    private void Awake()
    {
        Init(); //���� �ʱ�ȭ
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        ScaleCtrl();
    }
    private void Update()
    {
        if (dead) { return; }
        //���� �ִٸ� if�� ����
        if (isTarget)
        {
            //������ �����ϴٸ� if�� ����
            if (CanAttack())
            {
                anim.SetTrigger("2_Attack");
            }
        }
    }
    private void FixedUpdate()
    {
        if (dead) { return; }

        //Ÿ���� ���ٸ� �����̱�
        if (isTarget == false)
        {
            Move(Vector2.left);
            anim.SetBool("1_Move", true);
        }
        else anim.SetBool("1_Move", false);

        //���� �����ϰ� ������ �Ǹ� isTarget = true
        TargetScan(Vector2.left);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        GetComponent<CircleCollider2D>().enabled = false;
        //�ִϸ��̼� ó�� �κ�
        anim.SetTrigger("4_Death");
        anim.SetBool("isDeath", true);

    }
}
