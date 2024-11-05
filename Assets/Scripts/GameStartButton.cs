using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private void Awake()
    {
        MySceneManager.Instance.startButton = GameObject.Find("GameStart").GetComponent<Button>();
        MySceneManager.Instance.startButton.onClick.AddListener(MySceneManager.Instance.startButton.GetComponent<GameStartButton>().OnClick);
    }
    public void OnClick()
    {
        MySceneManager.Instance.GameStart();
    }
}
