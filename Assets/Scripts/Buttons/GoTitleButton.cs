using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoTitleButton : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        UIManager.Instance.quitButton.onClick.AddListener(UIManager.Instance.quitButton.GetComponent<GoTitleButton>().OnClick);
    }
    public void OnClick()
    {
        GameManager.Instance.LoadTitleScene();
    }
}
