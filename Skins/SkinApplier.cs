using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinApplier : MonoBehaviour
{
    public string currentSkin;
    public Renderer targetRenderer;

    private void Start()
    {
        ApplySkin(currentSkin);
    }

    public void ApplySkin(string skinName)
    {
        SkinData skin = SkinManager.Instance.GetSkin(skinName);
        if (skin != null && skin.isUnlocked)
        {
            targetRenderer.material = skin.skinMaterial;
        }
        else
        {
            Debug.LogWarning($"Скин {skinName} не найден или не разблокирован.");
        }
    }
}