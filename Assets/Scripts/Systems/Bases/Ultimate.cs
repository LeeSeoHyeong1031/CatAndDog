using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public ParticleSystem pt;
    public LayerMask targetLayer; //공격할 타겟
    public int damage = 100; //궁극기 데미지.
    public float ultimateInterval = 60f; //궁극기 간격.
    private float lastUltimateUse; //마지막 궁극기 사용 시간.

    public float wait;

    public Vector2 pointA = new Vector2(0, -20);
    public Vector2 pointB = new Vector2(50, 20);

    private void Start()
    {
        lastUltimateUse = ultimateInterval; //처음부터 궁극기를 사용할 순 없으니 궁극기 간격을 시작 값으로 줌.
    }

    private void Update()
    {
        UIManager.Instance.UltimateActive(ref lastUltimateUse);
    }

    //궁극기 사용 가능한지
    public void CanUltimateUse()
    {
        //60초 마다 궁극기 사용.
        if (Time.time >= lastUltimateUse)
        {
            lastUltimateUse = Time.time + ultimateInterval;
            UIManager.Instance.UltimateDeActive(); //파티클 끄고 궁극기 색상 원래대로 돌려 놓기.
            ParticleSystem pt1 = Instantiate(pt, pt.transform.position, pt.transform.rotation);
            pt1.Play();
            Destroy(pt1, 2f);
            StartCoroutine(UltimateUse());
        }
    }

    //궁극기 사용
    private IEnumerator UltimateUse()
    {
        yield return new WaitForSeconds(wait);
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
