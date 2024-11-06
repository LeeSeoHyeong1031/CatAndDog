using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    private Button levelUpButton;
    private void Awake()
    {
        levelUpButton = GetComponent<Button>();
        levelUpButton.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameManager.Instance.CanLevelUp();
    }
}
