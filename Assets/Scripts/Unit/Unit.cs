using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//모든 유닛의 기본 속성, 메소드
public class Unit : MonoBehaviour, ITakeDamage
{
	[Tooltip("유닛 스텟 데이터 입니다.")]
	[SerializeField] internal UnitData unitData;
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
	internal bool isSingleTarget; //단일 공격인지
	public LayerMask targetLayer; //타겟 레이어

	// spawn
	//internal int cost; //생산 비용

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

	//공격 메서드 (광역, 단일 업그레이드)
	//광역 공격
	public void AreaAttack()
	{
		foreach (Collider2D collider in colls)
		{
			collider.GetComponent<ITakeDamage>().TakeDamage(damage); //적, 기지 모두 포함해서 공격함
		}
	}

	//공격 메서드 (광역, 단일 업그레이드)
	//단일 공격
	public void SingleAttack()
	{
		int loweastHealth = int.MaxValue; //체력이 가장 낮은것을 담을 변수.
		Unit unit = null; //체력이 가장 낮은 Unit을 담을 변수.
		foreach (Collider2D collider in colls)
		{
			//공격하려는 곳에 Base가 있다면 기지 먼저 공격
			if (collider.TryGetComponent<Base>(out Base base1))
			{
				//적과, 기지가 같이 감지될 경우 기지만 단일로 때리고 return해서
				//기지가 아닌 Unit은 데미지를 입지 않음.
				base1.TakeDamage(damage);
				return;
			}
			//체력이 가장 낮은 적을 공격하는 로직
			else
			{
				if (collider.GetComponent<Unit>().health <= loweastHealth)
				{
					loweastHealth = collider.GetComponent<Unit>().health;
					unit = collider.GetComponent<Unit>();
				}
			}
		}
		unit.TakeDamage(damage); //foreach를 돌고 나온 체력이 가장 낮은 unit에게 TakeDamage()호출.
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
		//cost = unitData.cost;
		c_name = unitData.c_name;
		isSingleTarget = unitData.isSingleAttack;
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
