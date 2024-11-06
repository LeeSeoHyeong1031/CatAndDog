using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    private Button quitGameButton;

    private void Awake()
    {
        quitGameButton = GetComponent<Button>();
        quitGameButton.onClick.AddListener(() => MySceneManager.Instance.QuitGame());
    }

}
