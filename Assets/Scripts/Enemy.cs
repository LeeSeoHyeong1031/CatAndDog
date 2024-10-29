using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    void Update()
    {
        //Ÿ���� ���ٸ� �����̱�
        if (isTarget == false) Move();
    }

    public void Move()
    {
        transform.Translate(moveSpeed * Vector2.right * Time.deltaTime);
    }

    //�ݶ��̴� �� ���� �ִٸ� ������ �ֱ�.
    private void OnTriggerStay2D(Collider2D collision)
    {
        //���� �� ��� ���� ����� �ϴ� isTarget = true
        isTarget = true; // ���߱�
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        //TODO:������ �ٷ� ����. �ֳ��ϸ� �ʴ� 24�������� �����ϱ�. �ڷ��� WaitForSeconds �Ἥ �ʴ� ������ �߰��ϱ�
    }
}
