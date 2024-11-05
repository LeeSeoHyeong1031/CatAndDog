using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = 0)]
public class UnitData : ScriptableObject
{
    public string c_name; //ĳ���� �̸�
    public int health; //ü��
    public int damage; //���ݷ�
    public float attackInterval; //���ݼӵ�
    public int moveSpeed; //�̵� �ӵ�
    public int cost; //���� ���
    public Vector2Int attackRange; //���� ���� �Ÿ�
}
