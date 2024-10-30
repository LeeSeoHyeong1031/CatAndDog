using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Base : MonoBehaviour, ITakeDamage
{
    public int health;
    private int maxHealth;

    private void Start()
    {
        maxHealth = health;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        print($"{this.name}가 데미지를 입고 있음. 남은 체력: {health}/{maxHealth}");
        if (health <= 0)
        {
            health = 0;
            //기지가 부서짐
            print($"{this.name}가 부서짐");
            //승리/패배 UI 호출
        }
    }
}
