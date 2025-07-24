using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public static PetManager Instance;

    public List<PetInstance> equippedPets = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void EquipPet(PetData data)
    {
        if (equippedPets.Exists(p => p.petData == data)) return;

        GameObject obj = Instantiate(data.prefab, transform);
        PetInstance instance = obj.GetComponent<PetInstance>();
        instance.petData = data;
        equippedPets.Add(instance);
    }
public void UpgradePet(PetData data)
    {
        var pet = equippedPets.Find(p => p.petData == data);
        pet?.Upgrade();
    }

    public bool TryUpgradePet(PetData data)
    {
        var pet = equippedPets.Find(p => p.petData == data);
        if (pet != null && data.level < data.maxLevel)
        {
            pet.Upgrade();
            return true;
        }
        return false;
    }
}