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
			//�¸�/�й� UI ȣ��
			GameOver();
		}
		UIManager.Instance.UpdatePlayerBaseHeathText(); //�÷��̾� ���� ü��Text ����
	}
	public void GameOver()
	{
		GameManager.Instance.Stop();
		UIManager.Instance.Defeat(true);
	}
}
