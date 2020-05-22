using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{

    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject endLvl;
    public GameObject endGame;

    Teleport teleport;

    public GameObject lvlTwo;
    public GameObject lvlThree;


    private GameObject[] nextTeleport; 
    private int ch = 0;


    // Start is called before the first frame update
    void Start()
    {
    nextTeleport = new GameObject[] { lvlTwo, lvlThree };


    teleport = FindObjectOfType<Teleport>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;//замедление и ускорение времени
        GamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void LoadMenu()
    {
        Debug.Log("Load");
        SceneManager.LoadScene("Menu");
    }

    public void NewGame()
    {
        Debug.Log("Load");
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void NextLevel( )
    {
        Debug.Log("NextLevel");
        StartCoroutine(teleport.Tel(nextTeleport[ch]));
        ch++;
        endLvl.SetActive(false);

    }


    public void QuitGame()
    {
       // Debug.Log("Quit");
        Application.Quit();
    }
}
