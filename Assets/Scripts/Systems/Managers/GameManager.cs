using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{
    public List<Player> curPlayerUnits = new(); //���� ���ӿ� �����Ǿ� �ִ� �÷��̾� ����List
    public List<Enemy> curEnemyUnits = new(); //���� ���ӿ� �����Ǿ� �ִ� �� ����List

    public PlayerBase playerBase;
    public EnemyBase enemyBase;

    public PlayerBaseSpawner playerBaseSpawner;

    private CoinManager coinManager;

    public CoinManager getCoinManager() { return coinManager; }
    public void setCoinManager(CoinManager coinManager) { this.coinManager = coinManager; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q)) DoubleSpeedTime();
        else if (Input.GetKey(KeyCode.W)) Resume();
    }


    //�������� ��������.
    public void CanLevelUp()
    {
        //������ �ִ� ������ ���̺��� ������ ũ�ų� ���ٸ� ������ �ʰ������� return.
        if (coinManager.level >= coinManager.maxCoinByLevel.Length - 1)
        {
            return; //������ �� �� ����.  (���� ���� �ִ�ġ 6, maxCoinByLevel �ִ�ġ 6)
        }

        //������ ��ư�� �����µ� ���� ���� ������ ������ �ϱ� ���� �ڿ����� ���� ���.
        if (coinManager.curCoin < coinManager.costByLevelUp[coinManager.level])
        {
            return;
        }
        //���� ���� ������ ������ �ϱ� ���� �ڿ����� Ŭ ��� ������.
        else
        {
            //������ �ϴϱ� ���� ������ ������ ��� ��ŭ ���� �ؾ���.
            coinManager.curCoin -= coinManager.costByLevelUp[coinManager.level];
            LevelUp();
        }
    }

    //������ �޼���.
    private void LevelUp()
    {
        coinManager.level++;
        //����UI update, �������� �ʿ��� ���UI update.

        //Cost�� �ٸ� �迭�� ���̺��� �ϳ� �۱� ������ ������. �׷��� �ִ� ������ ���� ���� ��
        //UpdateCostText�� ȣ������ �ʰ� MaxLevel�� ���� ó��.
        if (coinManager.level == coinManager.maxCoinByLevel.Length - 1)
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

    public void DoubleSpeedTime()
    {
        Time.timeScale = 10;
    }

    //Ÿ��Ʋ �� �ҷ�����
    public void LoadTitleScene()
    {
        Init();
        MySceneManager.Instance.LoadTitleScene();
    }

    //���� �Ŵ��� ���� �ʵ� �ʱ�ȭ
    public void Init()
    {
        curPlayerUnits.Clear();
        curEnemyUnits.Clear();
        Resume();
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
