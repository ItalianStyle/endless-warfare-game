using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject mainMenu;

    [Header("Settings")]
    public GameObject settingsMenu;

    [Header("Exit")]
    public GameObject exitQuestion;

    public static MenuUI instance;


    private void Awake()
    {
        instance = this;
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("TestTank");
    }

    public void OnSettingsButton()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnReturnButton()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void OnQuitButton()
    {
        mainMenu.SetActive(false);
        exitQuestion.SetActive(true);
    }

    public void OnYesButton()
    {
        Application.Quit();
    }

    public void OnNoButton()
    {
        exitQuestion.SetActive(false);
        mainMenu.SetActive(true);
    }
}
