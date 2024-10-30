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
		//���� �ִٸ� if�� ����
		if (isTarget)
		{
			//������ �����ϴٸ� ���� �Ÿ� �ȿ� �ִ� colls ��� Attack().
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
