using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private void Update()
    {
        //Ÿ���� ���ٸ� �����̱�
        if (isTarget == false) Move();
    }

    public void Move()
    {
        transform.Translate(moveSpeed * Vector2.left * Time.deltaTime);
    }

    //�÷��̾� �ݶ��̴� �� ���� �ִٸ� ������ �ֱ�.
    private void OnTriggerStay2D(Collider2D collision)
    {
        //TODO:������ �ٷ� ����. �ֳ��ϸ� �ʴ� 24�������� �����ϱ�. �ڷ��� WaitForSeconds �Ἥ �ʴ� ������ �߰��ϱ�
        //���� �� ��� ���� ����� �ϴ� isTarget = true
        isTarget = true; // ���߱�
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    //������ ������ �ִ� �ڷ�ƾ //���� �̿�
    public IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(attackInterval);
    }
}
