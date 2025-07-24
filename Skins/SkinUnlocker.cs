using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUnlocker : MonoBehaviour
{
    public string skinToUnlock;

    public void Unlock()
    {
        SkinManager.Instance.UnlockSkin(skinToUnlock);
    }
}