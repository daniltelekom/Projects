using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;

    public enum ItemType { Tech, Material, WeaponSkin, PetPart, Consumable }
    public ItemType type;
}