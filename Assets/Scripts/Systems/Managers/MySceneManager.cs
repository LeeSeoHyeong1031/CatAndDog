using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : SingletonManager<MySceneManager>
{
    public void GameStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
