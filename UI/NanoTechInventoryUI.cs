using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NanoTechInventoryUI : MonoBehaviour
{
    public Transform slotContainer;
    public GameObject slotPrefab;
    public TextMeshProUGUI descriptionText;
    public Button equipButton;

    private NanoTechnology selectedTech;
    private NanoTechInventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<NanoTechInventory>();
        inventory.OnUpdated += RefreshUI;
        RefreshUI();
    }

    void RefreshUI()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);

        foreach (var tech in inventory.Equipped)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slot.GetComponentInChildren<TextMeshProUGUI>().text = tech.techName;
            slot.GetComponent<Button>().onClick.AddListener(() => OnTechSelected(tech));
        }
    }

    void OnTechSelected(NanoTechnology tech)
    {
        selectedTech = tech;
        descriptionText.text = tech.description;
        equipButton.interactable = true;
    }

    public void OnEquipPressed()
    {
        if (selectedTech != null)
        {
            inventory.Equip(selectedTech);
        }
    }
}