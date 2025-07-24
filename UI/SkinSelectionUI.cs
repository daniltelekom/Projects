using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinSelectionUI : MonoBehaviour
{
    public GameObject skinButtonPrefab;
    public Transform skinListParent;
    public TextMeshProUGUI skinNameText;
    public Image skinPreviewImage;
    public Button applyButton;

    private List<SkinData> availableSkins;
    private SkinData selectedSkin;

    public void Initialize(List<SkinData> skins)
    {
        availableSkins = skins;

        foreach (Transform child in skinListParent)
            Destroy(child.gameObject);

        foreach (var skin in availableSkins)
        {
            var btn = Instantiate(skinButtonPrefab, skinListParent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = skin.skinName;
            btn.GetComponent<Button>().onClick.AddListener(() => SelectSkin(skin));
        }
    }

    void SelectSkin(SkinData skin)
    {
        selectedSkin = skin;
        skinNameText.text = skin.skinName;
        skinPreviewImage.sprite = skin.preview;
        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(() => ApplySkin());
    }

    void ApplySkin()
    {
        SkinManager.Instance.ApplySkin(selectedSkin);
    }
}