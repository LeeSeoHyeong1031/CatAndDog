using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateButton : MonoBehaviour
{
    private Button ultimateButton;
    private void Awake()
    {
        ultimateButton = GetComponent<Button>();
        ultimateButton.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameManager.Instance.playerBaseSpawner.GetComponent<Ultimate>().CanUltimateUse();
    }
}
