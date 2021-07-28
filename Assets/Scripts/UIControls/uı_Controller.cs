using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uı_Controller : MonoBehaviour
{
    // Start Screen UI Elements
    [Header("Start Button")]
    public GameObject mainScreen;
    
    // Topbar UI elements 
    [Header("Topbar")]
    public GameObject topbar;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public Text score;
    public Text bestScore;

    [Header("Main Screen")]
    public Text mainHighScore;

    [Header("Shop")]
    public GameObject shopPanel;
    public GameObject podium;
    public shopController sc;


    // auido controller
    public AudioSource adSource;
    public AudioClip click;
    
    private void Awake()
    {
        // we will close the topbar when the game starts
        topbar.SetActive(false);
       
    }

    private void Start() {
       mainHighScore.text = PlayerPrefs.GetFloat("highScore").ToString();
    }

    private void Update()
    {

        // Checking game over happens if player controller bool game over we will stop the game and show game over panel
        if (PlayerController.gameOver == true)
        {
            gameOver();
        }
    }
   
    // Start button function
    public void tapToStart()
    {
        adSource.PlayOneShot(click);
        GameObject gm = GameObject.FindGameObjectWithTag("gm");
        gm.GetComponent<GameController>().ControllingSpawnController();
        Time.timeScale = 1;
        mainScreen.SetActive(false);
        topbar.SetActive(true);
        MoveController.gameStarted = true;
    }

    public void shop()
    {
        adSource.PlayOneShot(click);
        podium.SetActive(true);
        mainScreen.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void shopBack()
    {
        sc.checkModel();
        adSource.PlayOneShot(click);
        podium.SetActive(false);
        mainScreen.SetActive(true);
        shopPanel.SetActive(false);
    }

    // Pause menu Restart function
    public void restart()
    {
        adSource.PlayOneShot(click);
        PlayerController.gameOver = false;
        SceneManager.LoadScene("SampleScene");
    }

    // GameOver Function
    public void gameOver()
    {
        // stop time and score writing textbox
        topbar.SetActive(false);
        gameOverPanel.SetActive(true);
        score.text = GameController.score.ToString();
        bestScore.text = PlayerPrefs.GetFloat("highScore").ToString();
    }

    // Quit function
    public void quitGame()
    {
        adSource.PlayOneShot(click);
        Application.Quit();
    }

    
}
