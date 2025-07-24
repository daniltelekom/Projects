using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetUpgradeUI : MonoBehaviour
{
    public TMP_Text petNameText;
    public TMP_Text currentLevelText;
    public TMP_Text nextBonusText;
    public TMP_Text requiredMaterialsText;
    public Button upgradeButton;

    private PetData selectedPet;
    private PetManager petManager;

    private void Start()
    {
        petManager = FindObjectOfType<PetManager>();
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    public void DisplayPet(PetData pet)
    {
        selectedPet = pet;

        if (selectedPet == null) return;

        petNameText.text = selectedPet.petName;
        currentLevelText.text = $"Level: {selectedPet.level}/{selectedPet.maxLevel}";

        nextBonusText.text = selectedPet.level < selectedPet.maxLevel
            ? $"Next Buffs:\n{selectedPet.GetNextLevelBonusInfo()}"
            : "Max Level Reached";

        requiredMaterialsText.text = selectedPet.level < selectedPet.maxLevel
            ? $"Materials Needed:\n{selectedPet.GetRequiredMaterialsInfo()}"
            : "";
    }

    private void OnUpgradeButtonClicked()
    {
        if (selectedPet != null && petManager.TryUpgradePet(selectedPet))
        {
            DisplayPet(selectedPet); // Обновим отображение после апгрейда
        }
    }
}