using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameController : MonoBehaviour
{
    // Big Obstacle definition
    [Header("Big Obstacle System")]
    public GameObject[] bigObstacle;
    public static bool bigObstacleBool = false;

    // Obstacle Prefab & Position Definition
    [Header("Obstacle System")]
    public List<GameObject> obstacles;
    public List<GameObject> activeObstacles;
    public Transform ball;
    public int obsPosition;
    int totalobs=0;

    // Timer & Score Definition
    int min = 0;
    float sec = 0;
    float obstimer = 0;
    public float checkTimerforObstacle;
    float timerStart = 1;

    // UI Elements 
    [Header("UI Elements")]
    public Text scoreText;
    public Text suggestText;
    public GameObject suggestPanel;

    // Extra Definition
    public static float score;
  
    private void Awake()
    {
        bigObstacleBool = false;
    }

    // Start function
    private void Start()
    {
        InvokeRepeating("difficulty", 0, 30);
        InvokeRepeating("scoreCounter", 0, 0.02f);
    }

    // Update function
    private void Update()
    {

        // Timer function called
        if (Time.time >= timerStart)
        {
            Timer();
            timerStart = Time.time + 1f;
        }
        

        // Obstacle's timer starting
        if (MoveController.gameStarted)
        {
            obstimer += Time.deltaTime * 2;
            
            if (obstimer > checkTimerforObstacle)
            {
                if (totalobs < 6)
                {
                    obstimer = 0;
                    ControllingSpawnController();
                    totalobs++;
                }
                else
                {
                    bigObstacleSpawn();
                    totalobs = 0;
                }
                
            }
        }

    }

    // increasing difficulty
    void difficulty()
    {
        if (MoveController.gameStarted)
        {
            checkTimerforObstacle -= .15f;
            GameObject.FindWithTag("ball").GetComponent<MoveController>().forwardSpeed += 1;
        }
    }

    // Score counter function
    public void scoreCounter()
    {
        if (MoveController.gameStarted)
        {
            score = (float)Math.Round(ball.transform.position.z);
            Math.Round(score);
            scoreText.text = score.ToString() + "m";
        }
    }

    // Timer function
    void Timer()
    {
        if (MoveController.gameStarted)
        {
            sec += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(sec);
            if (sec >= 60)
            {
                min++;
                sec = 0;
            }
        }
    }

    // controlling SpawnController with timer
    public void ControllingSpawnController()
    {
        if (sec < 60)
        {
            if (bigObstacleBool == false)
            {
                Spawner();
            } 
           
        }
    }

    void Spawner()
    {
        int randomObstacle = UnityEngine.Random.Range(0, obstacles.Count);
        obstacles[randomObstacle].SetActive(true);
        obstacles[randomObstacle].transform.position = new Vector3(0, 0, ball.position.z + obsPosition);
        activeObstacles.Add(obstacles[randomObstacle]);
        obstacles.RemoveAt(randomObstacle);
        Invoke("RefreshList", 2);
    }
    
    void RefreshList()
    {
        obstacles.Add(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }

    // Big obstacle spawn function
    void bigObstacleSpawn()
    {
        if (MoveController.gameStarted)
        {
            Invoke("bigobstaclesbackbool", 0.25f);
            suggestPanel.SetActive(true);
            int random = UnityEngine.Random.Range(0, bigObstacle.Length);
            bigObstacle[random].SetActive(true);
            bigObstacle[random].transform.position = new Vector3(0, 0f, ball.position.z + obsPosition);
            suggestText.text = bigObstacle[random].GetComponent<bigObstaclesController>().suggest;
            bigObstacleBool = true;
        }
    }

    // checking big obstecle boolen value
    void bigobstaclesbackbool()
    {
        bigObstacleBool = false;
    }

}
