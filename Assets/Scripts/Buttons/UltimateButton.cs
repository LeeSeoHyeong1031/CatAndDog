using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateButton : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ultimateButton = GameObject.Find("UltimateBackground").GetComponent<Button>();
        UIManager.Instance.ultimateButton.onClick.AddListener(UIManager.Instance.ultimateButton.GetComponent<UltimateButton>().OnClick);
    }
    public void OnClick()
    {
        GameManager.Instance.playerBaseSpawner.GetComponent<Ultimate>().CanUltimateUse();
    }
}
