using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoTerminal : MonoBehaviour
{
    public NanoTechInventoryUI techUI;
    public int techOfferCount = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var inventory = other.GetComponent<NanoTechInventory>();
            if (inventory != null)
            {
                var offers = NanoTechManager.Instance.GenerateRandomTechnologies(techOfferCount);
                techUI.OpenUI(offers, inventory);
            }
        }
    }
}