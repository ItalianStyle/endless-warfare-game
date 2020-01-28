using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private bool mouseControls;
    public bool paused;
    
    private void Awake()
    {
        // if there is another istance and it's not us
        if (instance != null && instance != this)
        {           
            //destroy this instance 
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            // don't destroy the game manager (mainly to keep the mouseControls toggle) while changing the levels (aka scenes)
            DontDestroyOnLoad(gameObject);           
        }
    }

    public void TogglePauseGame()
    {
        paused = !paused;
        //Freeze the game if it's paused
        Time.timeScale = (paused == true) ? 0.0f : 1.0f;

        GameUI.instance.TogglePauseScreen(paused);
    }

    public bool GetMouseControls()
    {
        return mouseControls;
    }

    public void OnToggleMouseControls()
    {
        mouseControls = !mouseControls;
    }  
}
