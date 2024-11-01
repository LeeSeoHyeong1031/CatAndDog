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

    //궁극기 사용 가능한지
    public void CanUltimateUse()
    {
        //60초 마다 궁극기 사용.
        if (Time.time >= ultimateInterval)
        {
            ultimateInterval = Time.time + ultimateInterval;
            UltimateUse();
        }
    }

    //궁극기 사용
    private void UltimateUse()
    {
        // 해당 영역 내의 충돌체 감지
        Collider2D[] colliders = Physics2D.OverlapAreaAll(pointA, pointB, targetLayer);

        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Enemy>().TakeDamage(damage);
        }
    }



    void OnDrawGizmos()
    {
        // 영역 중심 좌표
        Vector2 center = (pointA + pointB) / 2;

        // 가로 세로 크기
        Vector2 size = new Vector2(Mathf.Abs(pointB.x - pointA.x), Mathf.Abs(pointB.y - pointA.y));

        // Gizmos 색상 설정
        Gizmos.color = Color.red;

        // 사각형 영역을 시각적으로 표시 (WireCube 사용)
        Gizmos.DrawWireCube(center, size);
    }
}
