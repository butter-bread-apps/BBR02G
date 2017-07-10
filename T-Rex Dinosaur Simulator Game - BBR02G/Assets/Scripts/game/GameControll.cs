﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControll : MonoBehaviour
{

    public static int coin;
    public static bool pause;
    public Text coine, distance;
    public Text goCoin, goScore, goBScore, lamlCount;
    public GameObject MainMenuPanel, GameOverPanel, powerPanel, SaveLifePanel;
    private AudioListener al;
    private int lcoin, lCount;
    private float timerl = 0;
    private bool lifesave = false;
    public GameObject saveButton;
    private Image sl;
    public static bool SaveMe = false, loadS;
    public static bool showAd = false;
    private bool changeScorecolor;
    private float cc = 0;

    void Start()
    {

        coin = 0;
        changeScorecolor = false;
        distance.color = new Color(1, 1, 1, 1);
        pause = false;
        showAd = false;
        al = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        al.enabled = true;
        if (ProtectedPrefs.HasKey("Coins"))
        {
            lcoin = ProtectedPrefs.GetInt("Coins");
        }
        else
        {
            lcoin = 0;
        }
        GetLamp();
        sl = saveButton.GetComponent<Image>();
    }

    void Update()
    {
        if (lifesave == true && Controller.iDie && !loadS && !SaveMe)
        {
            showAd = false;
            SaveLifePanel.SetActive(true);
            timerl += Time.deltaTime;
            sl.fillAmount -= Time.deltaTime / 1.8F;
            if (timerl >= 1.8F)
            {
                timerl = 0;
                sl.fillAmount = 1;
                SaveLifePanel.SetActive(false);
                Invoke("GameOver", 0.5f);
                loadS = true;
            }
        }

        if (itemAbsorb.getLamp) GetLamp();
        coine.text = coin.ToString();
        distance.text = Controller.Distance.ToString("00000000") + "0";
        if (Controller.Distance > 10 && Controller.Distance > ProtectedPrefs.GetFloat("HighScore") && !changeScorecolor)
        {
            cc += Time.deltaTime;
            if (cc < 1)
            {
                distance.color = new Color(1, 0, 0, 1);
            }
            else if (cc > 1 && cc < 1.5f)
            {
                distance.color = new Color(1, 1, 1, 1);
            }
            else if (cc > 1.5f && cc < 2)
            {
                distance.color = new Color(1, 0, 0, 1);
            }
            else if (cc > 2 && cc < 2.5f)
            {
                distance.color = new Color(1, 1, 1, 1);
            }
            else if (cc > 2.5f)
            {
                distance.color = new Color(1f, 0.92f, 0.016f, 1f);
                cc = 0;
                changeScorecolor = true;
            }

        }
        if (Controller.iDie && ProtectedPrefs.GetInt("mLamp") < 1)
        {
            MainMenuPanel.SetActive(false);
            Invoke("GameOver", 0.5f);
        }
        else if (Controller.iDie && ProtectedPrefs.GetInt("mLamp") > 0 && !lifesave)
        {
            MainMenuPanel.SetActive(false);
            lifesave = true;
            loadS = false;
        }
    }

    public void SaveLife()
    {
        SaveLifePanel.SetActive(false);
        ProtectedPrefs.SetInt("mLamp", lCount - 1);
        Controller.iDie = false;
        SaveMe = true;
        lifesave = false;
        GetLamp();
        MainMenuPanel.SetActive(true);
        timerl = 0;
        sl.fillAmount = 1;
    }

    public void GetLamp()
    {
        if (ProtectedPrefs.HasKey("mLamp"))
        {
            lCount = ProtectedPrefs.GetInt("mLamp");
        }
        else
        {
            lCount = 0;
        }
        itemAbsorb.getLamp = false;
        lamlCount.text = lCount.ToString();

    }

    private void GameOver()
    {
        GetComponent<AdmobManager>().ShowVideo();
        GameOverPanel.SetActive(true);
        powerPanel.SetActive(false);
        ProtectedPrefs.SetInt("Coins", lcoin + coin);
        goCoin.text = "Coins: " + coin.ToString();
        goScore.text = "Score: " + Controller.Distance.ToString("f0") + "0";
        if (ProtectedPrefs.GetInt("Coins") > 100)
        {
            Social.ReportProgress("CgkIu86qr5EIEAIQAQ", 100, (bool success) =>
             {

             });
        }
        else if (ProtectedPrefs.GetInt("Coins") > 250)
        {
            Social.ReportProgress("CgkIu86qr5EIEAIQAg", 100, (bool success) =>
            {

            });
        }
        else if (ProtectedPrefs.GetInt("Coins") > 500)
        {

            Social.ReportProgress("CgkIu86qr5EIEAIQAw", 100, (bool success) =>
            {

            });
        }
        else if (ProtectedPrefs.GetInt("Coins") > 750)
        {
            Social.ReportProgress("CgkIu86qr5EIEAIQBA", 100, (bool success) =>
            {

            });

        }
        else if (ProtectedPrefs.GetInt("Coins") > 1000)
        {
            Social.ReportProgress("CgkIu86qr5EIEAIQBQ", 100, (bool success) =>
            {

            });
        }

        Social.ReportScore(ProtectedPrefs.GetInt("Coins"), "CgkIu86qr5EIEAIQBg", (bool success) =>
        {

        });


        if (Controller.Distance > ProtectedPrefs.GetFloat("HighScore"))
        {
            ProtectedPrefs.SetFloat("HighScore", Controller.Distance);
            Social.ReportScore(ProtectedPrefs.GetInt("HighScore"), "CgkIu86qr5EIEAIQBw", (bool success) =>
            {

            });
        }
        if (ProtectedPrefs.HasKey("HighScore"))
        {
            goBScore.text = "Best Score: " + ProtectedPrefs.GetFloat("HighScore").ToString("f0") + "0";
        }
        else
        {
            goBScore.text = "Best Score: " + Controller.Distance.ToString("f0") + "0";
        }

    }

    public void isPause()
    {
        pause = true;
        al.enabled = false;
    }

    public void isContinue()
    {
        pause = false;
        al.enabled = true;
    }

    public void isRestart()
    {
        SceneManager.LoadScene("main");
    }

    public void isMainMenu()
    {
        pause = false;
        SceneManager.LoadScene("menu");
    }

    public void isLeaderboards()
    {
        if (Social.Active.localUser.authenticated) { Social.Active.ShowLeaderboardUI(); }
    }
}
