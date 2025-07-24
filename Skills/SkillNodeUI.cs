using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillNodeUI : MonoBehaviour
{
    public SkillNodeData data; // ScriptableObject с названием и описанием
    public Image icon;
    public Button button;
    public int currentLevel;

    public event Action<SkillNodeUI> OnNodeSelected;

    public void Select()
    {
        OnNodeSelected?.Invoke(this);
    }

    public bool CanUpgrade()
    {
        return currentLevel < data.maxLevel;
    }

    public void Upgrade()
    {
        if (CanUpgrade())
        {
            currentLevel++;
        }
    }
}