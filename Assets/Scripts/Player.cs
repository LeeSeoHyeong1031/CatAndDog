using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : Unit
{
	public Animator anim;

	private void Update()
	{
		if (dead) { return; }
		//적이 있다면 if문 실행
		if (isTarget)
		{
			//공격이 가능하다면 공격 거리 안에 있는 colls 모두 Attack().
			if (CanAttack())
			{
				Attack();
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
		anim.SetBool("isDeath", true);
		anim.SetTrigger("4_Death");
	}
}
