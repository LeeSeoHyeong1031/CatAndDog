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
        Init(); //스텟 초기화
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        ScaleCtrl();
    }
    private void Update()
    {
        if (dead) { return; }
        //적이 있다면 if문 실행
        if (isTarget)
        {
            //공격이 가능하다면 if문 실행
            if (CanAttack())
            {
                anim.SetTrigger("2_Attack");
            }
        }
    }
    private void FixedUpdate()
    {
        if (dead) { return; }

        //타겟이 없다면 움직이기
        if (isTarget == false)
        {
            Move(Vector2.left);
            anim.SetBool("1_Move", true);
        }
        else anim.SetBool("1_Move", false);

        //적을 감지하고 감지가 되면 isTarget = true
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
        //애니메이션 처리 부분
        anim.SetTrigger("4_Death");
        anim.SetBool("isDeath", true);

    }
}
