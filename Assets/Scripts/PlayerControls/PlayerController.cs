using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Coin UI definition
    int inLevelCoin = 0;
    int totalCoin;
    public Text coinCounter;
    public Text totalcoinText;
    public static bool gameOver = false;

    // Fat Level UI Elements 
    public GameObject[] fatLevelImages;
    public GameObject suggestPanel;

    // Ball size 
    public Vector3 scalePlus;

    // coin animation events
    public Animator collect;

    // efects
    public ParticleSystem collectcoinsefect;

    // audio controller
    public AudioSource adSource;
    public AudioClip gameOverClip;
    public AudioClip collectcoins;
    public AudioClip trueClip;

    // ADS CONTROLLER
    public adManager admanager;

    private void Start()
    {
        // inlevelcoin reset every round for counter and totalcoin getting playerprefs 
        inLevelCoin = 0;
        totalCoin = PlayerPrefs.GetInt("totalcoin");
        totalcoinText.text = " X " + "\n" +totalCoin.ToString();
    }
    private void Update()
    {
        totalCoin = PlayerPrefs.GetInt("totalcoin");
        totalcoinText.text = " X " + "\n" + totalCoin.ToString();
    }
    
    // We checking collision
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // checking tag control and writing function
        if (hit.gameObject.CompareTag("coin"))
        {
            adSource.PlayOneShot(collectcoins);
            Instantiate(collectcoinsefect, hit.transform.position, Quaternion.identity);
            inLevelCoin++;
            collect.SetTrigger("collect");
            coinCounter.text = inLevelCoin.ToString();
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("obstacle"))
        {
            // game over panel calling and total coin setting for main menu 
            admanager.adShow();
            adSource.PlayOneShot(gameOverClip);
            MoveController.gameStarted = false;
            totalCoin += inLevelCoin;
            PlayerPrefs.SetInt("totalcoin", totalCoin);
            gameOver = true;
            MoveController.gameStarted = false;
        }
        if (hit.gameObject.CompareTag("true"))
        {
            adSource.PlayOneShot(trueClip);
            suggestPanel.SetActive(false);
            Destroy(hit.gameObject);
        }

    }


}
