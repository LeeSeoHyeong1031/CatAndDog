using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.levelUpButton = GameObject.Find("LevelPanel").GetComponent<Button>();
        UIManager.Instance.levelUpButton.onClick.AddListener(UIManager.Instance.levelUpButton.GetComponent<LevelUpButton>().OnClick);
    }
    public void OnClick()
    {
        GameManager.Instance.CanLevelUp();
    }
}
