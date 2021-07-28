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
    public GameObject[] obstacles;
    public Transform ball;
    float left = -2f;
    float mid = 0;
    float right = 2;
    int obscount = 1;
    public int obsPosition;
    public float destroyTime;
    int totalobs=0;

    // Timer & Score Definition
    int min = 0;
    float sec = 0;
    float obstimer = 0;
    public float checkTimerforObstacle;

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
        // InvokeRepeating("bigObstacleSpawn", 5, 5);
        InvokeRepeating("difficulty", 0, 30);
        InvokeRepeating("scoreCounter", 0, 0.02f);
    }

    // Update function
    private void Update()
    {

        // Timer function called
        Timer();

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
            if (score > PlayerPrefs.GetFloat("highScore"))
            {
                PlayerPrefs.SetFloat("highScore", score);
            }
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
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        SpawnController(left);
                    }
                    if (i == 1)
                    {

                        SpawnController(mid);

                    }
                    if (i == 2)
                    {
                        SpawnController(right);

                    }

                }
            }
            obscount = 1;
        }
    }

    // Random spawn and way controller
    void SpawnController(float way)
    {
        int index = UnityEngine.Random.Range(0, 100);
        // if index smaller than 50 we'll spawn coin
        if (index < 30)
        {
            GameObject new_object = Instantiate(obstacles[0]);
            new_object.transform.position = new Vector3(way, 0.5f, ball.position.z + obsPosition);
            Destroy(new_object, destroyTime);
        }
        else
        {
            // if obscount smaller than 3, we will spawn obstacle
            if (obscount < 3)
            {
                int obs = UnityEngine.Random.Range(1, obstacles.Length);
                GameObject new_object = Instantiate(obstacles[obs]);
                new_object.transform.position = new Vector3(way, 0.5f, ball.position.z + obsPosition);
                Destroy(new_object, destroyTime);
                obscount++;
            }
            // if have been 2 obstacle we will spawn again coin, -1 or +1
            else
            {
                GameObject new_object = Instantiate(obstacles[0]);
                new_object.transform.position = new Vector3(way, 0.5f, ball.position.z + obsPosition);
                Destroy(new_object, destroyTime);
            }
        }
    }

    // Big obstacle spawn function
    void bigObstacleSpawn()
    {
        if (MoveController.gameStarted)
        {
            Invoke("bigobstaclesbackbool", 0.25f);
            suggestPanel.SetActive(true);
            GameObject bigObs = Instantiate(bigObstacle[UnityEngine.Random.Range(0, bigObstacle.Length)]);
            bigObs.transform.position = new Vector3(0, 0f, ball.position.z + obsPosition);
            string sug = bigObs.GetComponent<bigObstaclesController>().suggest;
            suggestText.text = sug;
            bigObstacleBool = true;
            Destroy(bigObs, destroyTime);
            
        }
    }

    // checking big obstecle boolen value
    void bigobstaclesbackbool()
    {
        bigObstacleBool = false;
    }

}
