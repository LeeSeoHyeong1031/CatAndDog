using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private Button gameStartButton;
    private void Awake()
    {
        gameStartButton = GetComponent<Button>();
        gameStartButton.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        MySceneManager.Instance.GameStart();
    }
}