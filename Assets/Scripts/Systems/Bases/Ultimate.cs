using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public LayerMask targetLayer;
    public int damage = 100;
    public float ultimateInterval = 60f;

    public Vector2 pointA = new Vector2(0, -20);
    public Vector2 pointB = new Vector2(50, 20);

    private Color origin;

    private void Start()
    {
        origin = UIManager.Instance.UltimateBackground.color;
    }

    private void Update()
    {
        UIManager.Instance.UpdateUltimateColor(ref ultimateInterval, ref origin);
    }

    //�ñر� ��� ��������
    public void CanUltimateUse()
    {
        //60�� ���� �ñر� ���.
        if (Time.time >= ultimateInterval)
        {
            ultimateInterval = Time.time + ultimateInterval;
            UltimateUse();
        }
    }

    //�ñر� ���
    private void UltimateUse()
    {
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
