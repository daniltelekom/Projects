using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{
    public Transform skinListContainer;
    public GameObject skinButtonPrefab;

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (SkinData skin in SkinManager.Instance.allSkins)
        {
            GameObject buttonObj = Instantiate(skinButtonPrefab, skinListContainer);
            Text btnText = buttonObj.GetComponentInChildren<Text>();
            btnText.text = skin.skinName + (skin.isUnlocked ? " (Unlocked)" : $" - {skin.cost}$");

            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(() => TryPurchaseSkin(skin.skinName));
        }
    }

    private void TryPurchaseSkin(string skinName)
    {
        SkinData skin = SkinManager.Instance.GetSkin(skinName);

        if (skin == null)
        {
            Debug.LogWarning("Скин не найден.");
            return;
        }

        if (skin.isUnlocked)
        {
            Debug.Log($"Скин {skinName} уже разблокирован.");
            return;
        }

        int playerMoney = PlayerPrefs.GetInt("Money", 0);
        if (playerMoney >= skin.cost)
        {
            playerMoney -= skin.cost;
            PlayerPrefs.SetInt("Money", playerMoney);
            SkinManager.Instance.UnlockSkin(skinName);
            Debug.Log($"Скин {skinName} куплен!");
        }
        else
        {
            Debug.Log("Недостаточно денег.");
        }
    }
}