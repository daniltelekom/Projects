using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeUI : MonoBehaviour
{
    public SkillNodeUI[] allNodes;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDesc;
    public Button upgradeButton;

    private SkillNodeUI currentNode;

    private void Start()
    {
        foreach (var node in allNodes)
        {
            node.OnNodeSelected += OnNodeSelected;
        }
    }

    void OnNodeSelected(SkillNodeUI node)
    {
        currentNode = node;
        skillName.text = node.data.skillName;
        skillDesc.text = node.data.description;
        upgradeButton.interactable = node.CanUpgrade();
    }
    public void OnUpgradePressed()
    {
        if (currentNode != null)
        {
            currentNode.Upgrade();
            OnNodeSelected(currentNode); // Обновить UI
        }
    }
}