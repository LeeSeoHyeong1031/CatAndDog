using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    public Player player;
    //ĳ���� �������� ������
    public GameObject P_SpawnCooltimeBar; //GameObject�� Ű�� �� ����
    public Image characterImage; //�������� �� �̹��� ������ ������ ����
    public Slider spawnCooltimeBar; //���� ��Ÿ�� ui�� ������ ����
    public TextMeshProUGUI characterCostText; //ĳ���� ��� Text

    public bool isSpawning; //���� ���� ������.

    private Button spawnButton;

    private void Awake()
    {
        spawnButton = GetComponent<Button>();
        spawnButton.onClick.AddListener(CanSpawn);
    }

    private void Start()
    {
        characterCostText.text = player.unitData.cost.ToString();
    }

    public void CanSpawn()
    {
        //���� ���� ������ ������ ������ ��뺸�� ���ٸ� ����.
        if (GameManager.Instance.curCoin >= player.unitData.cost && isSpawning == false)
        {
            isSpawning = true;
            SpawningImage();
            GameManager.Instance.curCoin -= player.unitData.cost;
            GameManager.Instance.playerBaseSpawner.Spawn(player);
        }
    }

    // ���� �� �� ��
    public void SpawningImage()
    {
        StartCoroutine(SpawningImageCoroutine());
    }

    private IEnumerator SpawningImageCoroutine()
    {
        float startTime = Time.time;
        P_SpawnCooltimeBar.gameObject.SetActive(true); // ������ �θ� ������Ʈ �ѱ�

        // ��Ÿ�� ���� �ٸ� ���̱�
        while (Time.time <= startTime + player.cooltime)
        {
            float elapsed = Time.time - startTime; //����ð� <- ���� ���� Ŀ��
            spawnCooltimeBar.value = Mathf.Lerp(1f, 0f, elapsed / player.cooltime);
            yield return null; // ���� �����ӱ��� ���
        }

        // �Ϸ�Ǹ� �ٸ� ����� �� �ʱ�ȭ �� �� ��
        P_SpawnCooltimeBar.gameObject.SetActive(false);
        spawnCooltimeBar.value = 1f;
        isSpawning = false;
    }

    //ĳ���� ��� ���� , ĳ���� ���� ����
    public void CharacterCostAndImageColor()
    {
        //������ ������ ������ ��뺸�� Ŭ ��
        if (GameManager.Instance.curCoin >= player.unitData.cost && isSpawning == false)
        {
            characterCostText.color = Color.black;
            characterImage.color = Color.white;
        }
        //������ ������ ������ ��뺸�� �۰� ���� ���� ��
        else if (isSpawning == true)
        {
            characterImage.color = Color.black;
        }
        //������ ������ ������ ��뺸�� �۰� ���� ���� �ƴ϶��
        else
        {
            characterCostText.color = Color.red;
            characterImage.color = Color.gray;
        }
    }

    private void Update()
    {
        CharacterCostAndImageColor();
    }
}
