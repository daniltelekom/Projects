using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventory;
    public GameObject itemSlotPrefab;
    public Transform slotContainer;

    private void OnEnable()
    {
        inventory.OnChanged += RefreshUI;
        RefreshUI();
    }

    private void OnDisable()
    {
        inventory.OnChanged -= RefreshUI;
    }

    void RefreshUI()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);

        foreach (var item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, slotContainer);
            slot.GetComponentInChildren<TMP_Text>().text = item.itemName;
            slot.GetComponent<Image>().sprite = item.icon;
        }
    }
}