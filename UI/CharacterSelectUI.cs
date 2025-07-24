using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Transform characterListContainer;
    public GameObject characterButtonPrefab;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public GameObject characterModel;

    public Button selectButton;

    public TextMeshProUGUI[] statTexts;
    public Image[] skillIcons;
    public TextMeshProUGUI[] skillNames;
    public TextMeshProUGUI[] skillDescriptions;

    private CharacterData currentCharacter;

    private void Start()
    {
        PopulateCharacterList();
    }

    void PopulateCharacterList()
    {
        foreach (var data in CharacterDatabase.AllCharacters)
        {
            GameObject btn = Instantiate(characterButtonPrefab, characterListContainer);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = data.characterName;

            CharacterData localData = data; // ВАЖНО: чтобы избежать замыкания
            btn.GetComponent<Button>().onClick.AddListener(() => OnCharacterSelected(localData));
        }
    }

    void OnCharacterSelected(CharacterData data)
    {
        currentCharacter = data;

        characterName.text = data.characterName;
        characterIcon.sprite = data.icon;

        if (characterModel != null)
            Destroy(characterModel);

        characterModel = Instantiate(data.modelPrefab, transform);

        for (int i = 0; i < statTexts.Length && i < data.baseStats.Length; i++)
        {
            statTexts[i].text = data.baseStats[i].ToString();
        }

        for (int i = 0; i < skillIcons.Length && i < data.skills.Length; i++)
        {
            if (data.skills[i] != null)
            {
                skillIcons[i].sprite = data.skills[i].icon;
                skillNames[i].text = data.skills[i].name;
                skillDescriptions[i].text = data.skills[i].description;
            }
        }
    }

    public void OnSelectPressed()
    {
        if (currentCharacter != null)
        {
            PlayerSaveData.currentSave.selectedSkinId = currentCharacter.id;
            Debug.Log("Выбран персонаж: " + currentCharacter.characterName);
        }
    }
}