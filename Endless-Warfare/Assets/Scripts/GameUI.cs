using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public SimpleHealthBar healthBar;
    public TextMeshProUGUI hp;

    [Header("Pause Menu")]
    public GameObject pauseMenu;

    [Header("Settings")]
    public GameObject settingsMenu;
    public Toggle localMouseControls;
    
    
    //instance
    public static GameUI instance;

    private void Awake()
    {
        //set the instance to this script
        instance = this;
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        UpdateToggle();
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P)) && GameManager.instance.paused == false)
        {
            GameManager.instance.TogglePauseGame();
        }

    }

    public void UpdateToggle()
    {
        localMouseControls.isOn = GameManager.instance.GetMouseControls() == true;
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePauseGame();
    }

    public void OnSettingsButton()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnGoBackButton()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void OnMainMenuButton()
    {
        GameManager.instance.TogglePauseGame();
        SceneManager.LoadScene(0);
    }

    public void TogglePauseScreen(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    public void ToggleMouseControls()
    {
        GameManager.instance.OnToggleMouseControls();
    } 
}
