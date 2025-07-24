using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechDropUIManager : MonoBehaviour
{
    public GameObject techButtonPrefab;
    public Transform leftColumn;
    public Transform rightColumn;

    public void ShowTechnologies(List<NanoTechnology> techs)
    {
        Clear();

        for (int i = 0; i < techs.Count; i++)
        {
            Transform parent = (i < 10) ? leftColumn : rightColumn;
            GameObject btn = Instantiate(techButtonPrefab, parent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = techs[i].techName;
            btn.GetComponent<Button>().onClick.AddListener(() => ApplyTech(techs[i]));
        }
    }

    void ApplyTech(NanoTechnology tech)
    {
        // Логика применения технологии
        Debug.Log($"Применена: {tech.techName}");
    }

    void Clear()
    {
        foreach (Transform child in leftColumn) Destroy(child.gameObject);
        foreach (Transform child in rightColumn) Destroy(child.gameObject);
    }
}