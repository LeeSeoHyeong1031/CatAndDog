using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TitleEnemy : Unit
{
    private Animator anim;

    //EnemyBaseSpawner에서 사용 할 변수들
    public float spawnInterval; //스폰간격
    [Tooltip("몇 분 뒤에 스폰할 지 결정하는 변수입니다.")]
    public float startSpawnTime; //처음 스폰 시간.
    [HideInInspector] public float lastSpawnTime; //마지막 스폰 시간

    private void Awake()
    {
        Init(); //스텟 초기화
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        ScaleCtrl(); //localeScale 설정
    }
    private void Update()
    {
        if (dead) { return; }

        //적이 있다면 if문 실행
        if (isTarget == true)
        {
            //공격이 가능하다면 공격 거리 안에 있는 colls 모두 Attack().
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
            Move(Vector2.right);
            anim.SetBool("1_Move", true);
        }
        else anim.SetBool("1_Move", false);

        //적을 감지하고 감지가 되면 isTarget = true
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
        GetComponent<CircleCollider2D>().enabled = false;
        //애니메이션 처리 부분
        anim.SetTrigger("4_Death");
        anim.SetBool("isDeath", true);

    }
}
