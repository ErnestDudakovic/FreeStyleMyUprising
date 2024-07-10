using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Add this line to include TextMeshProUGUI

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[7, 7]; // Update the size of the array
    public float coins;
    public Text CoinsTXT;
    public bool purchaseLocked = false;

    private CoinManager coinManager;
    public GameObject shopTextPopupObject; // Assign in inspector
    public TextMeshProUGUI shopTextPopupText; // Assign in inspector

    void Start()
    {
        coinManager = CoinManager.instance;

        if (coinManager != null)
        {
            coins = coinManager.coinCount;
            UpdateCoinsText();
            InitializeShopItems();
            LoadPurchases(); // Load purchases on start
        }
        else
        {
            Debug.LogWarning("CoinManager instance not found!");
        }

        //RESET SPREMLJENIH STATOVA I COINA
        // ResetPlayerPrefs();
    }

    void InitializeShopItems()
    {
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5; // Critical chance upgrade item ID
        shopItems[1, 6] = 6; // Critical power upgrade item ID

        shopItems[2, 1] = 5;
        shopItems[2, 2] = 7;
        shopItems[2, 3] = 5;
        shopItems[2, 4] = 15;
        shopItems[2, 5] = 25; // Price for critical chance upgrade
        shopItems[2, 6] = 30; // Price for critical power upgrade

        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0; // Initial quantity for critical chance upgrade
        shopItems[3, 6] = 0; // Initial quantity for critical power upgrade
    }

    void UpdateCoinsText()
    {
        CoinsTXT.text = "Coins: " + coins.ToString();
    }

    public void Buy(int itemID)
    {
        if (!purchaseLocked)
        {
            if (coins >= shopItems[2, itemID])
            {
                int previousQuantity = shopItems[3, itemID]; // Get previous quantity
                coins -= shopItems[2, itemID];
                shopItems[3, itemID]++;
                UpdateCoinsText();
                UpdateItemQuantityText(itemID);

                PlayerStats playerStats = FindObjectOfType<PlayerStats>();
                if (playerStats != null)
                {
                    if (itemID == 1)
                    {
                        playerStats.IncreaseHealthByTen();
                    }
                    else if (itemID == 2)
                    {
                        playerStats.IncreaseDamageByThirty();
                    }
                    else if (itemID == 3) // Armor upgrade
                    {
                        playerStats.IncreaseArmorByFive();
                    }
                    else if (itemID == 4) // Dash speed upgrade
                    {
                        playerStats.IncreaseDashSpeedByHalf();
                    }
                    else if (itemID == 5) // Critical chance upgrade
                    {
                        playerStats.IncreaseCritChanceByOne();
                    }
                    else if (itemID == 6) // Critical power upgrade
                    {
                        playerStats.IncreaseCritPowerByTen();
                    }
                }
                else
                {
                    Debug.LogError("PlayerStats component not found on the player GameObject!");
                }

                SavePurchases();
                DisplayShopTextPopup("Previous quantity: " + previousQuantity.ToString() +
                                      "\nNew: " + shopItems[3, itemID].ToString(), itemID);
            }
            else
            {
                Debug.Log("Not enough coins to purchase item.");
            }
            coinManager.UpdateCoinCount((int)coins);
        }
        else
        {
            Debug.Log("Purchase is locked. Cannot buy more items.");
        }
    }

    public void Sell(int itemID)
    {
        if (shopItems[3, itemID] > 0)
        {
            coins += shopItems[2, itemID];
            shopItems[3, itemID]--;
            UpdateCoinsText();
            UpdateItemQuantityText(itemID);

            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats != null)
            {
                if (itemID == 1)
                {
                    playerStats.DecreaseHealthByTen();
                }
                else if (itemID == 2)
                {
                    playerStats.DecreaseDamageByThirty();
                }
                else if (itemID == 3) // Armor downgrade
                {
                    playerStats.DecreaseArmorByFive();
                }
                else if (itemID == 4) // Dash speed downgrade
                {
                    playerStats.DecreaseDashSpeedByHalf();
                }
                else if (itemID == 5) // Critical chance downgrade
                {
                    playerStats.DecreaseCritChanceByOne();
                }
                else if (itemID == 6) // Critical power downgrade
                {
                    playerStats.DecreaseCritPowerByTen();
                }
            }
            else
            {
                Debug.LogError("PlayerStats component not found on the player GameObject!");
            }

            SavePurchases();
            // Display popup message
            DisplayShopTextPopup("Previous quantity: " + (shopItems[3, itemID] + 1).ToString() +
                                      "\nNew quantity: " + shopItems[3, itemID].ToString(), itemID);
        }
        else
        {
            Debug.Log("No items to sell.");
        }
    }

    void UpdateItemQuantityText(int itemID)
    {
        GameObject itemObject = GameObject.Find("Item" + itemID);
        if (itemObject != null)
        {
            ButtonInfo buttonInfo = itemObject.GetComponent<ButtonInfo>();
            if (buttonInfo != null)
            {
                buttonInfo.QuantityTxt.text = shopItems[3, itemID].ToString();
            }
            else
            {
                Debug.LogWarning("ButtonInfo component not found on item object.");
            }
        }
        else
        {
            Debug.LogWarning("Item object not found.");
        }
    }

    public void LogPurchase()
    {
        for (int i = 1; i < shopItems.GetLength(1); i++)
        {
            Debug.Log("Item ID: " + i + ", Quantity: " + shopItems[3, i]);
        }
        Debug.Log("Remaining coins: " + coins);
    }

    public void ReturnToHUB()
    {
        SceneManager.LoadScene(1);
    }

    // Save purchases to PlayerPrefs
    void SavePurchases()
    {
        for (int i = 1; i < shopItems.GetLength(1); i++)
        {
            PlayerPrefs.SetInt("Item" + i, shopItems[3, i]);
        }
        PlayerPrefs.Save();
    }

    // Load purchases from PlayerPrefs
    void LoadPurchases()
    {
        for (int i = 1; i < shopItems.GetLength(1); i++)
        {
            if (PlayerPrefs.HasKey("Item" + i))
            {
                shopItems[3, i] = PlayerPrefs.GetInt("Item" + i);
            }
        }
    }

    void DisplayShopTextPopup(string message, int itemID)
    {
        if (shopTextPopupObject != null && shopTextPopupText != null)
        {
            // Append current player stats to the message
            message += "\n\nCurrent Stats:";
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats != null)
            {
                if (itemID == 1)
                {
                    message += "\nHealth: " + playerStats.GetCurrentHealth();
                }
                else if (itemID == 2)
                {
                    message += "\nDamage: " + playerStats.GetCurrentDamage();
                }
                else if (itemID == 3)
                {
                    message += "\nArmor: " + playerStats.GetCurrentArmor();
                }
                else if (itemID == 4)
                {
                    message += "\nDash Speed: " + playerStats.GetCurrentDashSpeed();
                }
                else if (itemID == 5)
                {
                    message += "\nCritical Chance: " + playerStats.GetCurrentCritChance();
                }
                else if (itemID == 6)
                {
                    message += "\nCritical Power: " + playerStats.GetCurrentCritPower();
                }
            }
            else
            {
                Debug.LogError("PlayerStats component not found on the player GameObject!");
            }

            // Set the text of the popup and activate it
            shopTextPopupText.text = message;
            shopTextPopupObject.SetActive(true);

            // Start coroutine to hide the popup after some time
            StartCoroutine(HideShopTextPopup());
        }
        else
        {
            Debug.LogWarning("Shop text popup object or text component not assigned in the inspector!");
        }
    }

    IEnumerator HideShopTextPopup()
    {
        yield return new WaitForSeconds(2f); // Adjust delay as needed
        if (shopTextPopupObject != null)
        {
            shopTextPopupObject.SetActive(false);
        }
    }
}
