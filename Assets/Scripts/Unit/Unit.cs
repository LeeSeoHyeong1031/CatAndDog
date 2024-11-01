using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//모든 유닛의 기본 속성, 메소드
public class Unit : MonoBehaviour, ITakeDamage
{
    [Tooltip("유닛 스텟 데이터 입니다.")]
    [SerializeField] private UnitData unitData;
    //takedamage
    internal int health; //체력


    // move
    internal int moveSpeed; //이동 속도

    //attack
    internal int damage; //공격력
    internal Vector2Int attackRange; //공격 사정 거리
    internal float attackInterval; //공격속도
    internal float lastAttackTime = 0; //마지막 공격 시간
    internal bool isTarget = false; //적이 감지되면 멈추기
    public LayerMask targetLayer; //타겟 레이어

    // spawn
    internal int cost; //생산 비용

    // 캐릭터 스케일
    public float characterScale;

    internal bool dead; //플레이어 죽음
    internal string c_name; //캐릭터 이름

    private Rigidbody2D rb;
    protected Collider2D[] colls;
    private Vector2 boxCenter;
    private Vector2 boxSize;

    //데미지 받는 메서드
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        print($"{c_name}의 남은 체력 : {health}");
    }

    public virtual void Die()
    {
        health = 0;
        dead = true;
        Destroy(this.gameObject, 3f);
    }

    //적 감지 메서드
    public void TargetScan(Vector2 dir)
    {
        boxCenter = (Vector2)transform.position + dir * 4; // 박스 Pivot 왼쪽으로 4만큼 이동
        colls = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0, targetLayer);
        if (colls.Length > 0) isTarget = true;
        else isTarget = false;
    }

    //공격 할 수 있는지
    public bool CanAttack()
    {
        if (Time.time >= lastAttackTime)
        {
            lastAttackTime = Time.time + attackInterval;
            return true;
        }
        else return false;
    }

    //공격 메서드
    public void Attack()
    {
        foreach (Collider2D collider in colls)
        {
            if (collider.TryGetComponent<ITakeDamage>(out ITakeDamage other))
            {
                other.TakeDamage(damage);
            }
        }
    }

    //이동 메서드
    public void Move(Vector2 dir)
    {
        rb.MovePosition(rb.position + (moveSpeed * dir * Time.fixedDeltaTime));
    }

    //모든 초기화 담당. //추후에 세분화 가능
    public void Init()
    {
        //스텟 초기화
        health = unitData.health;
        damage = unitData.damage;
        attackInterval = unitData.attackInterval + 0.5f;
        attackRange = unitData.attackRange;
        moveSpeed = unitData.moveSpeed;
        cost = unitData.cost;
        c_name = unitData.c_name;
        //컴포넌트 할당
        rb = GetComponent<Rigidbody2D>();
        //BoxCenter,BoxSize 초기화
        boxSize.x = attackRange.x;
        boxSize.y = attackRange.y;
    }

    public void ScaleCtrl()
    {
        if (this.CompareTag("Player"))
        {
            transform.localScale = new Vector3(characterScale, characterScale, characterScale);
        }
        else
        {
            transform.localScale = new Vector3(-characterScale, characterScale, characterScale);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
