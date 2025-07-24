using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    private int currentAmount;

    void Start()
    {
        currentAmount = 0;
        UpdateUI();
    }

    public void SetCurrency(int amount)
    {
        currentAmount = amount;
        UpdateUI();
    }

    public void AddCurrency(int amount)
    {
        currentAmount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        currencyText.text = $" {currentAmount}";
    }
}