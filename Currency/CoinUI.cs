using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text coinText;
    private int coins = 0;

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coins.ToString();
    }

    public int GetCoinCount() => coins;
}