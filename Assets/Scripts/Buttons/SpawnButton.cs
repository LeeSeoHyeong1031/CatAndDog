using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
	public Player player;
	//캐릭터 스폰관련 변수들
	public GameObject P_SpawnCooltimeBar; //GameObject를 키고 끌 변수
	public Image characterImage; //스폰중일 때 이미지 색상을 조절할 변수
	public Slider spawnCooltimeBar; //스폰 쿨타임 ui를 구현할 변수
	public TextMeshProUGUI characterCostText; //캐릭터 비용 Text

	public bool isSpawning; //현재 스폰 중인지.

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
	private void Update()
	{
		CharacterCostAndImageColor();
	}

	public void CanSpawn()
	{
		CoinManager cm = GameManager.Instance.getCoinManager();

		//현재 가진 코인이 스폰할 유닛의 비용보다 높다면 스폰.
		if (cm.curCoin >= player.unitData.cost && isSpawning == false)
		{
			isSpawning = true;
			SpawningImage();
			cm.curCoin -= player.unitData.cost;
			GameManager.Instance.playerBaseSpawner.Spawn(player);
		}
	}

	// 스폰 중 일 때
	public void SpawningImage()
	{
		StartCoroutine(SpawningImageCoroutine());
	}

	private IEnumerator SpawningImageCoroutine()
	{
		float startTime = Time.time;
		P_SpawnCooltimeBar.gameObject.SetActive(true); // 스폰바 부모 오브젝트 켜기

		// 쿨타임 동안 바를 줄이기
		while (Time.time <= startTime + player.cooltime)
		{
			float elapsed = Time.time - startTime;
			spawnCooltimeBar.value = Mathf.Lerp(1f, 0f, elapsed / player.cooltime);
			yield return null; // 다음 프레임까지 대기
		}

		// 완료되면 바를 숨기기 및 초기화 할 것 들
		P_SpawnCooltimeBar.gameObject.SetActive(false);
		spawnCooltimeBar.value = 1f;
		isSpawning = false;
	}

	//캐릭터 비용 색상 , 캐릭터 색상 조정
	public void CharacterCostAndImageColor()
	{
		CoinManager cm = GameManager.Instance.getCoinManager();
		//소지한 코인이 유닛의 비용보다 클 때
		if (cm.curCoin >= player.unitData.cost && isSpawning == false)
		{
			characterCostText.color = Color.black;
			characterImage.color = Color.white;
		}
		//스폰 중일 때
		else if (isSpawning == true)
		{
			characterImage.color = Color.black;
		}
		//소지한 코인이 유닛의 비용보다 작고 스폰 중이 아니라면
		else
		{
			characterCostText.color = Color.red;
			characterImage.color = Color.gray;
		}
	}
}