using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public GameObject bonusWavePrefab;
    public float chanceToTrigger = 0.3f;
    public float countdown = 20f;

    private bool eventTriggered = false;

    public void TryStartRandomEvent()
    {
        if (!eventTriggered && Random.value <= chanceToTrigger)
        {
            StartCoroutine(BonusWaveTimer());
        }
    }

    IEnumerator BonusWaveTimer()
    {
        eventTriggered = true;
        Debug.Log("Таймер запущен на бонусную волну: " + countdown + " сек.");
        yield return new WaitForSeconds(countdown);

        Instantiate(bonusWavePrefab, Vector3.zero, Quaternion.identity);
        Debug.Log("Бонусная волна началась!");
        eventTriggered = false;
    }
}