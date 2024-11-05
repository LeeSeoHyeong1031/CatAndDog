using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public ParticleSystem pt;
    public LayerMask targetLayer; //������ Ÿ��
    public int damage = 100; //�ñر� ������.
    public float ultimateInterval = 60f; //�ñر� ����.
    private float lastUltimateUse; //������ �ñر� ��� �ð�.

    public float wait;

    public Vector2 pointA = new Vector2(0, -20);
    public Vector2 pointB = new Vector2(50, 20);

    private void Start()
    {
        lastUltimateUse = ultimateInterval; //ó������ �ñر⸦ ����� �� ������ �ñر� ������ ���� ������ ��.
    }

    private void Update()
    {
        UIManager.Instance.UltimateActive(ref lastUltimateUse);
    }

    //�ñر� ��� ��������
    public void CanUltimateUse()
    {
        //60�� ���� �ñر� ���.
        if (Time.time >= lastUltimateUse)
        {
            lastUltimateUse = Time.time + ultimateInterval;
            UIManager.Instance.UltimateDeActive(); //��ƼŬ ���� �ñر� ���� ������� ���� ����.
            ParticleSystem pt1 = Instantiate(pt, pt.transform.position, pt.transform.rotation);
            pt1.Play();
            Destroy(pt1, 2f);
            StartCoroutine(UltimateUse());
        }
    }

    //�ñر� ���
    private IEnumerator UltimateUse()
    {
        yield return new WaitForSeconds(wait);
        // �ش� ���� ���� �浹ü ����
        Collider2D[] colliders = Physics2D.OverlapAreaAll(pointA, pointB, targetLayer);

        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Enemy>().TakeDamage(damage);
        }
    }



    void OnDrawGizmos()
    {
        // ���� �߽� ��ǥ
        Vector2 center = (pointA + pointB) / 2;

        // ���� ���� ũ��
        Vector2 size = new Vector2(Mathf.Abs(pointB.x - pointA.x), Mathf.Abs(pointB.y - pointA.y));

        // Gizmos ���� ����
        Gizmos.color = Color.red;

        // �簢�� ������ �ð������� ǥ�� (WireCube ���)
        Gizmos.DrawWireCube(center, size);
    }
}
