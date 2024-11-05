using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Base
{
	private void Awake()
	{
		GameManager.Instance.playerBase = this;
		maxHealth = health;
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
		UIManager.Instance.UpdatePlayerBaseHeathText(); //플레이어 기지 체력Text 업뎃
	}
	public void GameOver()
	{
		GameManager.Instance.Stop();
		UIManager.Instance.Defeat(true);
	}
}
