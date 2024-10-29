using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = 0)]
public class UnitData : ScriptableObject
{
    public int health; //ü��
    public int damage; //���ݷ�
    public float attackInterval; //���ݼӵ�
    public int attackRange; //���� ���� �Ÿ�
    public int moveSpeed; //�̵� �ӵ�
    public int cost; //���� ���
}
