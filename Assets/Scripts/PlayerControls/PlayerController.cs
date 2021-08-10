using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
    // Coin UI definition
    int inLevelCoin = 0;
    int totalCoin;
    [Header("UI Elements")]
    public Text coinCounter;
    public Text totalcoinText;
    public static bool gameOver = false;
    public GameObject suggestPanel;
    public uý_Controller UIController;

    // coin animation events
    [Header("Animator")]
    public Animator collect;

    // efects
    [Header("Particle Efects")]
    public List<ParticleSystem> CollectCoinEffects;

    // audio controller
    [Header("Audio Clips")]
    public AudioSource adSource;
    public AudioClip gameOverClip;
    public AudioClip collectcoins;
    public AudioClip trueClip;

    [Header("ADS MANAGER")]
    public AppodealAdsManager appodealAdsManager;

    private void Start()
    {
        // inlevelcoin reset every round for counter and totalcoin getting playerprefs 
        inLevelCoin = 0;
        MainScreenCoinControl();
    }
    
    public void MainScreenCoinControl()
    {
       totalCoin = PlayerPrefs.GetInt("totalcoin");  
       totalcoinText.text = " X " + "\n" + totalCoin.ToString();
    }

    // We checking collision
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.CompareTag("road"))
        {
            hit.transform.parent.transform.position += new Vector3(0, 0, 30);
        }

        // checking tag control and writing function
        if (hit.gameObject.CompareTag("coin"))
        {
            hit.gameObject.SetActive(false);
            adSource.PlayOneShot(collectcoins);
            int random = Random.Range(0, CollectCoinEffects.Count);
            CollectCoinEffects[random].transform.position = hit.transform.position;
            CollectCoinEffects[random].Play();
            inLevelCoin++;
            collect.SetTrigger("collect");
            coinCounter.text = inLevelCoin.ToString();
            StartCoroutine(OnVisible(hit.gameObject));
        }
        if (hit.gameObject.CompareTag("obstacle"))
        {
            // game over panel calling and total coin setting for main menu 
            if (GameController.score > PlayerPrefs.GetFloat("highScore"))
            {
                PlayerPrefs.SetFloat("highScore", GameController.score);
            }
            appodealAdsManager.adsShow();
            adSource.PlayOneShot(gameOverClip);
            MoveController.gameStarted = false;
            totalCoin += inLevelCoin;
            PlayerPrefs.SetInt("totalcoin", totalCoin);
            gameOver = true;
            UIController.gameOver(gameOver);
            MoveController.gameStarted = false;
        }
        if (hit.gameObject.CompareTag("true"))
        {
            hit.gameObject.SetActive(false);
            adSource.PlayOneShot(trueClip);
            suggestPanel.SetActive(false);
            StartCoroutine(OnVisible(hit.gameObject));
        }
    }
    IEnumerator OnVisible(GameObject gameObject)
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(true);
    }

}
