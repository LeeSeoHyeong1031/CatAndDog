using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//모든 유닛의 기본 속성, 메소드
public class Unit : MonoBehaviour
{
    [Tooltip("유닛 스텟 데이터 입니다.")]
    [SerializeField] private UnitData unitData;
    internal int health; //체력
    internal int damage; //공격력
    internal float attackInterval; //공격속도
    internal int attackRange; //공격 사정 거리
    internal int moveSpeed; //이동 속도
    internal int cost; //생산 비용
    internal bool isTarget = false; //적이 감지되면 멈추기
    private void Start()
    {
        //스텟 초기화
        health = unitData.health;
        damage = unitData.damage;
        attackInterval = unitData.attackInterval;
        attackRange = unitData.attackRange;
        moveSpeed = unitData.moveSpeed;
        cost = unitData.cost;
    }

    //데미지 받는 메서드
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //죽었을 때 처리
            GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
