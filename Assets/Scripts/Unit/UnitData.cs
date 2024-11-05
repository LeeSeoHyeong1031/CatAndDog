using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = 0)]
public class UnitData : ScriptableObject
{
    public string c_name; //캐릭터 이름
    public int health; //체력
    public int damage; //공격력
    public float attackInterval; //공격속도
    public int moveSpeed; //이동 속도
    public int cost; //생산 비용
    public Vector2Int attackRange; //공격 사정 거리
}
