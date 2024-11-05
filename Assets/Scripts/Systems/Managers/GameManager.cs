using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : SingletonManager<GameManager>
{
    public List<Player> curPlayerUnits = new(); //���� ���ӿ� �����Ǿ� �ִ� �÷��̾� ����List
    public List<Enemy> curEnemyUnits = new(); //���� ���ӿ� �����Ǿ� �ִ� �� ����List

    public PlayerBase playerBase;
    public EnemyBase enemyBase;

    public PlayerBaseSpawner playerBaseSpawner;

    //���ΰ��� ������
    public int curCoin = 0; //���� ����
    public int[] maxCoinByLevel = { 100, 150, 200, 250, 300, 350, 400 }; //���� �� �ִ� �ִ� ����
    private int[] coinRaiseByLevel = { 6, 10, 14, 18, 22, 26, 30 }; //�ʴ� ������ ���η�
    private float lastCoinRaiseTime = 0; //������ ������ ���� �ð�
    public int[] costByLevelUp = { 40, 80, 120, 160, 200, 240 }; //������ �� ���.
    public int level = 0;

    private void Update()
    {
        UpdateCoin(); //1�ʸ��� ������Ʈ.
        UpdateLevelUpCostColor();
    }

    //���� ������Ʈ �޼���.
    public void UpdateCoin()
    {
        //������ �ʴ� ������ ����
        if (Time.time >= lastCoinRaiseTime)
        {
            lastCoinRaiseTime = Time.time + 1f; //������ ������ ���� �ð��� 1�� �� ������. <- 1�ʰ� ���� ������ ������Ʈ ����.
            curCoin += coinRaiseByLevel[level]; //���������� ���η� ����.
            curCoin = Mathf.Clamp(curCoin, 0, maxCoinByLevel[level]); //curCoin�� ��� �����ִµ� ���� �������� ���� �� �ִ� �ִ������� �Ѿ��
            //�˾Ƽ� ��.
            UIManager.Instance.UpdateCoinText(ref curCoin, ref maxCoinByLevel[level]); //����Text UI������Ʈ
        }
    }

    //Cost 
    public void UpdateLevelUpCostColor()
    {
        //������ �ִ�ġ �� ��� return
        if (level == maxCoinByLevel.Length - 1) return;
        UIManager.Instance.costText.color = curCoin >= costByLevelUp[level] ? Color.black : Color.red;
    }

    //�������� ��������.
    public void CanLevelUp()
    {
        //������ �ִ� ������ ���̺��� ������ ũ�ų� ���ٸ� ������ �ʰ������� return.
        if (level >= maxCoinByLevel.Length - 1)
        {
            return; //������ �� �� ����.  (���� ���� �ִ�ġ 6, maxCoinByLevel �ִ�ġ 6)
        }

        //������ ��ư�� �����µ� ���� ���� ������ ������ �ϱ� ���� �ڿ����� ���� ���.
        if (curCoin < costByLevelUp[level])
        {
            return;
        }
        //���� ���� ������ ������ �ϱ� ���� �ڿ����� Ŭ ��� ������.
        else
        {
            //������ �ϴϱ� ���� ������ ������ ��� ��ŭ ���� �ؾ���.
            curCoin -= costByLevelUp[level];
            LevelUp();
        }
    }

    //������ �޼���.
    private void LevelUp()
    {
        level++;
        //����UI update, �������� �ʿ��� ���UI update.

        //Cost�� �ٸ� �迭�� ���̺��� �ϳ� �۱� ������ ������. �׷��� �ִ� ������ ���� ���� ��
        //UpdateCostText�� ȣ������ �ʰ� MaxLevel�� ���� ó��.
        if (level == maxCoinByLevel.Length - 1)
        {
            UIManager.Instance.MaxLevel();
            return;
        }
        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateCostText();
    }

    //���� ����
    public void Resume()
    {
        Time.timeScale = 1;
    }

    //�Ͻ� ����
    public void Stop()
    {
        Time.timeScale = 0;
    }

    //Ÿ��Ʋ �� �ҷ�����
    public void LoadTitleScene()
    {
        MySceneManager.Instance.LoadTitleScene();
    }

    //���� �Ŵ��� ���� �ʵ� �ʱ�ȭ
    public void localInit()
    {
        level = 0;
        curPlayerUnits.Clear();
        curEnemyUnits.Clear();
        curCoin = 0;
    }

    //���� ���� �� ��� �ʱ�ȭ
    public void AllInit()
    {
        localInit();
    }

    //GameManager�� �ִ� curPlayerUnits�� Add�� �� ����Ʈ�ȿ��� y��ǥ ���� �����ؼ� �־���� ��.
    //���� y��ǥ ������ �������� ����. �׸��� �� ���ĵ� List�� �ִ� ��� Layer���� ���������� +1�� �ο�.
    public void DescendingSortList<T>(List<T> list) where T : MonoBehaviour
    {
        List<T> u_List = new List<T>();

        while (u_List.Count < list.Count)
        {
            float maxY = float.MinValue;
            T bigUnit = null;

            foreach (T unit in list)
            {
                // �̹� �߰��� �÷��̾�� �ǳʶٱ�
                if (u_List.Contains(unit)) continue;

                // y���� ���� ū �÷��̾� ã��
                if (unit.transform.position.y > maxY)
                {
                    maxY = unit.transform.position.y;
                    bigUnit = unit;
                }
            }

            if (bigUnit != null)
            {
                u_List.Add(bigUnit);
            }
        }

        // ���ĵ� ����Ʈ�� curPlayerUnits�� ������Ʈ
        for (int i = 0; i < u_List.Count; i++)
        {
            list[i] = u_List[i];
            list[i].GetComponent<SortingGroup>().sortingOrder = i; // ���������� ���̾� �� �ο�
        }
    }
}
