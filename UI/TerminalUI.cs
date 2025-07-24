using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalUI : MonoBehaviour
{
    public GameObject terminalPanel;
    public Button openButton;
    public Button closeButton;

    public GameObject skillsTab;
    public GameObject nanoTechTab;
    public GameObject skinTab;

    public Button skillsButton;
    public Button nanoButton;
    public Button skinButton;

    void Start()
    {
        openButton.onClick.AddListener(Open);
        closeButton.onClick.AddListener(Close);

        skillsButton.onClick.AddListener(() => SwitchTab(skillsTab));
        nanoButton.onClick.AddListener(() => SwitchTab(nanoTechTab));
        skinButton.onClick.AddListener(() => SwitchTab(skinTab));

        terminalPanel.SetActive(false);
    }

    void Open()
    {
        terminalPanel.SetActive(true);
        SwitchTab(skillsTab);
    }

    void Close()
    {
        terminalPanel.SetActive(false);
    }

    void SwitchTab(GameObject tabToActivate)
    {
        skillsTab.SetActive(false);
        nanoTechTab.SetActive(false);
        skinTab.SetActive(false);
        tabToActivate.SetActive(true);
    }
}