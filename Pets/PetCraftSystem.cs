using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class PetCraftSystem : MonoBehaviour
{
    public PetData[] availablePets;
    public GameObject petPrefab;
    public Transform spawnPoint;

    public ResourceManager resourceManager;
    public InventorySystem inventorySystem;

    public void CraftPet(PetData petData)
    {
        if (petData == null || !inventorySystem.HasBlueprint(petData.petBlueprint)) return;

        if (!resourceManager.HasResources(petData.craftCost)) return;

        resourceManager.ConsumeResources(petData.craftCost);
        inventorySystem.RemoveBlueprint(petData.petBlueprint);

        GameObject petGO = Instantiate(petPrefab, spawnPoint.position, Quaternion.identity);
        PetInstance instance = petGO.GetComponent<PetInstance>();
        instance.petData = petData;
    }
}