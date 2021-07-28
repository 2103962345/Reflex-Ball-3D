using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundController : MonoBehaviour
{
    bool muted = false;
    public Image onvoice;
    public Image offvoice;
    

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            loadSettings() ;
        }
        updateIcon();
        AudioListener.pause = muted;
    }
    private void Update()
    {
        updateIcon();
    }
    private void updateIcon()
    {
        if (muted == false)
        {
            onvoice.enabled = true;
            offvoice.enabled = false;
        }
        else
        {
            onvoice.enabled = false;
            offvoice.enabled = true;
        }
    }

    public void audioController()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        saveSettings();
    }
    private void loadSettings()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void saveSettings()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
