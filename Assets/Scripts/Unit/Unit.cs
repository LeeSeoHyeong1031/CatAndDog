using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//��� ������ �⺻ �Ӽ�, �޼ҵ�
public class Unit : MonoBehaviour, ITakeDamage
{
    [Tooltip("���� ���� ������ �Դϴ�.")]
    [SerializeField] private UnitData unitData;
    //takedamage
    internal int health; //ü��


    // move
    internal int moveSpeed; //�̵� �ӵ�

    //attack
    internal int damage; //���ݷ�
    internal Vector2Int attackRange; //���� ���� �Ÿ�
    internal float attackInterval; //���ݼӵ�
    internal float lastAttackTime = 0; //������ ���� �ð�
    internal bool isTarget = false; //���� �����Ǹ� ���߱�
    public LayerMask targetLayer; //Ÿ�� ���̾�

    // spawn
    internal int cost; //���� ���

    // ĳ���� ������
    public float characterScale;

    internal bool dead; //�÷��̾� ����
    internal string c_name; //ĳ���� �̸�

    private Rigidbody2D rb;
    protected Collider2D[] colls;
    private Vector2 boxCenter;
    private Vector2 boxSize;

    //������ �޴� �޼���
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        print($"{c_name}�� ���� ü�� : {health}");
    }

    public virtual void Die()
    {
        health = 0;
        dead = true;
        Destroy(this.gameObject, 3f);
    }

    //�� ���� �޼���
    public void TargetScan(Vector2 dir)
    {
        boxCenter = (Vector2)transform.position + dir * 4; // �ڽ� Pivot �������� 4��ŭ �̵�
        colls = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0, targetLayer);
        if (colls.Length > 0) isTarget = true;
        else isTarget = false;
    }

    //���� �� �� �ִ���
    public bool CanAttack()
    {
        if (Time.time >= lastAttackTime)
        {
            lastAttackTime = Time.time + attackInterval;
            return true;
        }
        else return false;
    }

    //���� �޼���
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

    //�̵� �޼���
    public void Move(Vector2 dir)
    {
        rb.MovePosition(rb.position + (moveSpeed * dir * Time.fixedDeltaTime));
    }

    //��� �ʱ�ȭ ���. //���Ŀ� ����ȭ ����
    public void Init()
    {
        //���� �ʱ�ȭ
        health = unitData.health;
        damage = unitData.damage;
        attackInterval = unitData.attackInterval + 0.5f;
        attackRange = unitData.attackRange;
        moveSpeed = unitData.moveSpeed;
        cost = unitData.cost;
        c_name = unitData.c_name;
        //������Ʈ �Ҵ�
        rb = GetComponent<Rigidbody2D>();
        //BoxCenter,BoxSize �ʱ�ȭ
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
