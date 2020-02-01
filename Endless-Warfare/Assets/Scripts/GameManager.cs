using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private bool mouseControls = false;
    public bool paused;
    
    private void Awake()
    {
        // if there is another istance and it's not us
        if (instance != null && instance != this)
        {
            Debug.Log("Distruggo il gameObject");
            //destroy this instance 
            Destroy(gameObject);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 0)
        {

            instance = this;
            // don't destroy the game manager (mainly to keep the mouseControls toggle) while changing the levels (aka scenes)
            DontDestroyOnLoad(gameObject);           
        }
        else
        {
            instance = this;
            //Debug.Log("Siamo nella scena TestTank, mantengo il GameManager");
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
