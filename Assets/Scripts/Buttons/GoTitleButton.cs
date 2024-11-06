using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoTitleButton : MonoBehaviour
{
    private Button goTitleButton;
    private void Awake()
    {
        goTitleButton = GetComponent<Button>();
        goTitleButton.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        GameManager.Instance.LoadTitleScene();
    }
}
