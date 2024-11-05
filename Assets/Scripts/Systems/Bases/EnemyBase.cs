using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Base
{
	private Animator anim;

	private void Awake()
	{
		GameManager.Instance.enemyBase = this;
		maxHealth = health;
		anim = GetComponent<Animator>();
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);

		if (health <= 0)
		{
			health = 0;
			dead = true;
			//승리/패배 UI 호출
			GameOver();
		}
		UIManager.Instance.UpdateEnemyBaseHeathText(); //적 기지 체력Text 업뎃
	}
	public void GameOver()
	{
		anim.SetTrigger("Die");
		anim.SetBool("isDeath", true);
		GameManager.Instance.Stop();
		UIManager.Instance.Victory(true);
	}
}
