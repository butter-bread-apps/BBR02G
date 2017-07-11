using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionMenu : MonoBehaviour
{

    public Sprite[] choiceImages;
    public int[] prices;
    public Image targetImage;
    public Button BuyButton;
    public Text BuyButtonText;
    public GameObject IAP, choicesPanel;
    int currentChoice, confirmedChoice;
    bool player2Unlocked, player3Unlocked, player4Unlocked;

    public Text coinsText;

    void Start()
    {

    }
    //Checks which players are unlocked and which player the player has currently selected
    private void OnEnable()
    {
        if (!ProtectedPrefs.HasKey("PlayerChoice"))
        {
            ProtectedPrefs.SetInt("PlayerChoice", 0);
        }
        else if (ProtectedPrefs.HasKey("PlayerChoice"))
        {
            confirmedChoice = ProtectedPrefs.GetInt("PlayerChoice");
        }


        if (ProtectedPrefs.HasKey("Player2Unlocked"))
        {
            player2Unlocked = true;
        }
        if (ProtectedPrefs.HasKey("Player3Unlocked"))
        {
            player3Unlocked = true;
        }
        if (ProtectedPrefs.HasKey("Player4Unlocked"))
        {
            player4Unlocked = true;
        }
        BuyButton.gameObject.SetActive(true);
        currentChoice = 0;
       targetImage.sprite = choiceImages[currentChoice];
        UpdateUI();
    }


    public void SwitchChoice(bool next)
    {
        if (next)
        {
            if (currentChoice >= choiceImages.Length - 1)
            {
                currentChoice = 0;
            }
            else if (currentChoice < choiceImages.Length - 1)
            {
                currentChoice++;

            }
        }
        //if it's the "previous", next would be false, so we check and lower the choice number
        else if (!next)
        {
            if (currentChoice > 0)
            {
                currentChoice--;

            }
            else if (currentChoice <= 0)
            {
                currentChoice = choiceImages.Length - 1;
            }
        }
        UpdateUI();
    }

    public void AttemptSelection()
    {
        switch (currentChoice)
        {
            case 0:
                confirmedChoice = 0;
                ProtectedPrefs.SetInt("PlayerChoice", 0);

                UpdateUI();
                break;
            case 1:
                if (player2Unlocked)
                {
                    confirmedChoice = 1;
                    ProtectedPrefs.SetInt("PlayerChoice", 1);

                }
                else if(!player2Unlocked)
                {
                    int currentCoins = ProtectedPrefs.GetInt("Coins");
                    if (currentCoins >= prices[currentChoice])
                    {
                        currentCoins -= prices[currentChoice];
                        Debug.Log(currentCoins);
                        player2Unlocked = true;
                        ProtectedPrefs.SetString("Player2Unlocked", "true");
                        ProtectedPrefs.SetInt("PlayerChoice", 1);
                        confirmedChoice = 1;
                    }else
                    {
                        IAP.SetActive(true);
                        BuyButton.gameObject.SetActive(false);
                        choicesPanel.SetActive(false);

                    }

                }
                UpdateUI();
                break;
            case 2:
                if (player3Unlocked)
                {
                    confirmedChoice = 2;
                    ProtectedPrefs.SetInt("PlayerChoice", 2);

                }
                else if (!player3Unlocked)
                {
                    int currentCoins = ProtectedPrefs.GetInt("Coins");
                    if (currentCoins >= prices[currentChoice])
                    {
                        currentCoins -= prices[currentChoice];
                        Debug.Log(currentCoins);
                        player2Unlocked = true;
                        ProtectedPrefs.SetString("Player3Unlocked", "true");
                        ProtectedPrefs.SetInt("PlayerChoice", 2);
                        confirmedChoice = 2;
                    }
                    else
                    {
                        IAP.SetActive(true);
                        choicesPanel.SetActive(false);
                        BuyButton.gameObject.SetActive(false);
                       // gameObject.SetActive(false);
                    }

                }
                UpdateUI();
                break;
            case 3:
                if (player3Unlocked)
                {
                    confirmedChoice = 3;
                    ProtectedPrefs.SetInt("PlayerChoice", 3);

                }
                else if (!player3Unlocked)
                {
                    int currentCoins = ProtectedPrefs.GetInt("Coins");
                    if (currentCoins >= prices[currentChoice])
                    {
                        currentCoins -= prices[currentChoice];
                        Debug.Log(currentCoins);
                        player3Unlocked = true;
                        ProtectedPrefs.SetString("Player4Unlocked", "true");
                        ProtectedPrefs.SetInt("PlayerChoice", 3);
                        confirmedChoice = 3;
                    }
                    else
                    {
                        IAP.SetActive(true);
                        choicesPanel.SetActive(false);
                        BuyButton.gameObject.SetActive(false);
                        // gameObject.SetActive(false);
                    }

                }
                UpdateUI();
                break;


        }


    }

    void UpdateUI()
    {
        coinsText.text = ProtectedPrefs.GetInt("Coins").ToString();
          targetImage.sprite = choiceImages[currentChoice];
        if (currentChoice == confirmedChoice)
        {
            BuyButtonText.text = "Selected";

        }
        else
        {
            if(currentChoice == 0)
            {
                BuyButtonText.text = "Select";
            }
            if (currentChoice == 1 )
            {
                if(player2Unlocked)  BuyButtonText.text = "Select";
                else
                {
                    BuyButtonText.text = prices[currentChoice].ToString();

                }

            }
            else if (currentChoice == 2 )
            {
               if(player3Unlocked) BuyButtonText.text = "Select";
                else
                {
                    BuyButtonText.text = prices[currentChoice].ToString();

                }

            }else if(currentChoice == 4)
            {
                if (player4Unlocked) BuyButtonText.text = "Select";
                else
                {
                    BuyButtonText.text = prices[currentChoice].ToString();

                }
            }
           
        }

    }
}
