using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopController : MonoBehaviour
{
    
    public int currentBall = 0;
    public GameObject[] ballModels;
    public ballsModels[] balls;
    [Header("UI ELEMENTS")]
    public Button buyButton;
    public Button equipButton;
    public Text counter;
    public Text price;
    public Text coins;
    public AudioSource adSource;
    public AudioClip click;
    public AudioClip equipClip;
   

    private void Start()
    {
        coins.text = PlayerPrefs.GetInt("totalcoin").ToString();
        foreach (ballsModels model in balls)
        {
            if (model.price == 0)
            {
                model.isPurchased = true;
            }
            else
            {
                model.isPurchased = PlayerPrefs.GetInt(model.name, 0) == 0 ? false:true ;
               
            }
        }
        checkModel();
    }
    
    public void checkModel()
    {
        currentBall = PlayerPrefs.GetInt("currentBall", 0);
        foreach (GameObject ball in ballModels)
        {
            ball.SetActive(false);
            ballModels[currentBall].SetActive(true);
        }
        updateUI();
    }

    public void equipmodel()
    {
        
        adSource.PlayOneShot(equipClip);
        equipButton.GetComponentInChildren<Text>().text = "EQUIPED";
        ballsModels ball = balls[currentBall];
        if (!ball.isPurchased)
        {
            return;
        }
        PlayerPrefs.SetInt("currentBall", currentBall);
        updateUI();
    }

    public void nextModel()
    {
        
        equipButton.GetComponentInChildren<Text>().text = "EQUIP";
        adSource.PlayOneShot(click);
        ballModels[currentBall].SetActive(false);
        currentBall++;
        if (currentBall == ballModels.Length)
        {
            currentBall = 0;
        }
        ballModels[currentBall].SetActive(true);
        updateUI();
    }

    public void backModel()
    {
        equipButton.GetComponentInChildren<Text>().text = "EQUIP";
        adSource.PlayOneShot(click);
        ballModels[currentBall].SetActive(false);
        currentBall--;
        if (currentBall == -1)
        {
            currentBall = ballModels.Length - 1 ;
        }
        ballModels[currentBall].SetActive(true);
        updateUI();
    }

    public void unlockModel()
    {
        adSource.PlayOneShot(click);
        ballsModels ball = balls[currentBall];
        PlayerPrefs.SetInt(ball.name, 1);
        PlayerPrefs.SetInt("currentBall", currentBall);
        ball.isPurchased = true;
        PlayerPrefs.SetInt("totalcoin", PlayerPrefs.GetInt("totalcoin", 0) - ball.price);
        updateUI();
    }

    private void updateUI()
    {
        ballsModels ball = balls[currentBall];
        if (ball.isPurchased)
        {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
            
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);
            price.text = ball.price.ToString();
            if (ball.price < PlayerPrefs.GetInt("totalcoin",0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
        counter.text = currentBall + 1 + " / " + ballModels.Length;
        coins.text = PlayerPrefs.GetInt("totalcoin").ToString();
    }
}
